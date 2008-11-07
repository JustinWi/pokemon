using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Threading;
using System.IO;
using FTP;

namespace PokeMon
{
    class PokeFtpAuditTask : ITask
    {
        public PokeFtpAuditTask(string server, string username, string password)
            : this(server, username, password, long.MaxValue, long.MaxValue)
        {
        }

        public PokeFtpAuditTask(string server, string username, string password, long diskSpaceWarningLevel, long diskSpaceFailureLevel)
        {
            this.server = server;
            this.user = username;
            this.password = password;

            this.diskSpaceWarningLevel = diskSpaceWarningLevel;
            this.diskSpaceFailureLevel = diskSpaceFailureLevel;

            if (diskSpaceWarningLevel > diskSpaceFailureLevel)
            {
                throw new ArgumentException(string.Format("Disk space warning level ({0}) cannot be greater than the disk space failure level {1}", diskSpaceWarningLevel, diskSpaceFailureLevel));
            }
        }

        public virtual Result Perform()
        {
            try
            {
                Result.ResultValue returnResult = Result.ResultValue.Pass;
                returnDescription = "";

                ftpClient = new FTP.FTPclient(server, user, password);

                // clear out files from any previous lookups
                files.Clear();
                totalFileSize = 0;

                AddFileData("");
                GetPreviousFiles();

                // Check to see if we're over of disk space utilization limits
                if (totalFileSizeInMB >= diskSpaceFailureLevel)
                {
                    returnResult = Result.ResultValue.Fail;
                    returnDescription += String.Format("Disk space has exceeded {0} mb, currently at {1}.", diskSpaceFailureLevel, totalFileSizeInMB);
                }
                else if (totalFileSizeInMB >= diskSpaceWarningLevel)
                {
                    returnResult = Result.ResultValue.Warning;
                    returnDescription += String.Format("Disk space has exceeded {0} mb but below {1}, currently at {2} mb.", diskSpaceWarningLevel, diskSpaceFailureLevel, totalFileSizeInMB);
                }
                else
                {
                    returnResult = Result.ResultValue.Pass;
                    returnDescription += String.Format("Disk space is below warning threshold of {0} mb.  Currently at {1} mb.", diskSpaceWarningLevel, totalFileSizeInMB);
                }

                // See if there are any changes to our files since the last time we looked.
                if (!CompareFiles())
                {
                    // There are changes. Serialize the new file list for comparison next time
                    SaveFiles();

                    // We must be careful not to downgrade any results from previous checks
                    if (returnResult < Result.ResultValue.Warning)
                    {
                        returnResult = Result.ResultValue.Warning;
                    }

                    returnDescription += "\n\nFiles have changed!" + comparisonResults;
                }
                else
                {
                    returnDescription += "\n\nFiles have not changed.";
                }

                return new Result(ActionName, returnResult, returnDescription);
            }
            catch (WebException exc)
            {
                return new Result(ActionName, Result.ResultValue.Warning, "Exception encountered: " + exc.ToString());
            }
            finally
            {
                // nulling this guy out appears to get it cleaned up which prevents the FTP server from going
                // into passive mode.
                ftpClient = null;
            }
        }

        private void SaveFiles()
        {
            Stream stream = File.Open(TempFileName, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, files);
            stream.Close();
        }

        private void GetPreviousFiles()
        {
            Stream stream = File.Open(TempFileName, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            prevFiles = (List<FTPfileInfo>)bFormatter.Deserialize(stream);
            stream.Close();
        }

        private bool CompareFiles()
        {
            comparisonResults = findNewMissingUpdatedFiles();

            return comparisonResults == "";
        }

        private string findNewMissingUpdatedFiles()
        {
            List<FTPfileInfo> missingFiles = new List<FTPfileInfo>();
            List<FTPfileInfo> newFiles = new List<FTPfileInfo>();

            string comparisonStr = "";

            // fill the missing files list with our previous list of files
            missingFiles.AddRange(prevFiles);

            // Walk over each of our new files.  Anything we don't find in the "missing files" list must be new
            // Anything that's left over must actually be a missing file.
            foreach (FTPfileInfo fileInfo in files)
            {
                if (missingFiles.Contains(fileInfo))
                {
                    FTPfileInfo prevFile = missingFiles.Find(new Predicate<FTPfileInfo>(fileInfo.Equals));

                    // If the old and new files don't match perfectly, the new one must have been updated
                    if (fileInfo.CompareTo(prevFile) != 0)
                    {
                        comparisonStr += "\n\n--- " + fileInfo.FullName + " has Changed ---\n" +
                            "Previous version:\n" + prevFile.ToString() + "\n\n" +
                            "Current version:\n" + fileInfo.ToString();
                    }

                    // remove the file from the list of potential missing files
                    missingFiles.Remove(fileInfo);
                }
                else
                {
                    newFiles.Add(fileInfo);
                }
            }

            if (missingFiles.Count > 0)
            {
                comparisonStr += "\n\n---------- Missing Files -----------";

                foreach (FTPfileInfo file in missingFiles)
                {
                    comparisonStr += "\n\n" + file.ToString();
                }
            }

            if (newFiles.Count > 0)
            {
                comparisonStr += "\n\n----------- New Files -----------";

                foreach (FTPfileInfo file in newFiles)
                {
                    comparisonStr += "\n\n" + file.ToString();
                }
            }

            return comparisonStr;
        }

        private void AddFileData(string path)
        {
            const int MaxTries = 3;

            FTPdirectory ftpDir = new FTPdirectory();

            // If the path is //. or //.. don't store it
            if (path.EndsWith("."))
            {
                return;
            }

            // Looks like we're getting some random timeout failures.  Putting in this loop
            // so we'll retry a couple times before giving up.
            for (int numTries = 0; numTries < MaxTries; numTries++)
            {
                try
                {
                    // Get all the files and directories in this directory
                    ftpDir = ftpClient.ListDirectoryDetail(path);

                    // If we didn't get an exception, get out of this loop
                    break;
                }
                catch (WebException exc)
                {
                    // If we've tried a lot already, give up
                    if (numTries >= MaxTries - 1)
                    {
                        throw exc;
                    }
                    else
                    {
                        // Wait a little while and try again.
                        Thread.Sleep(1000);
                    }
                }
            }

            // Store each of the files in this directory
            ftpDir.GetFiles("").ForEach(new Action<FTPfileInfo>(StoreFiles));

            // Recursively call this function on 
            ftpDir.GetDirectories().ForEach(new Action<FTPfileInfo>(AddFileData));
        }

        private void AddFileData(FTPfileInfo fileInfo)
        {
            AddFileData(fileInfo.FullName);
        }

        private void StoreFiles(FTPfileInfo fileInfo)
        {
            // Add file to audit list
            files.Add(fileInfo);

            // Sum up file sizes
            totalFileSize += fileInfo.Size;
        }

        protected FTPclient ftpClient;

        private List<FTPfileInfo> files = new List<FTPfileInfo>();
        private List<FTPfileInfo> prevFiles = new List<FTPfileInfo>();

        private long totalFileSize;
        private long diskSpaceWarningLevel;
        private long diskSpaceFailureLevel;

        protected string server;
        protected string user;
        protected string password;
        protected string comparisonResults;
        protected string returnDescription;

        protected const string tempFilePrefix = "Temp - ";
        protected const string tempFileSuffix = ".bin";

        protected string TempFileName
        {
            get { return ((string)(tempFilePrefix + ActionName)).Replace(".", "_").Replace(":", "").Replace("//", "") + tempFileSuffix; }
        }

        protected long totalFileSizeInMB
        {
            get { return totalFileSize / (1024 * 1024); }
        }

        // Name of the action - used by notifiers
        protected virtual string ActionName
        {
            get { return "FTP File Audit - " + server; }
        }
    }
}
