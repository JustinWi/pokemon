using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace PokeMon
{
    class PokeFtpFileTask : PokeFtpAuditTask
    {
        public PokeFtpFileTask(string server, string username, string password, string fileName) 
            : base(server, username, password)
        {
            this.fileName = fileName;
        }

        public override Result Perform()
        {
            try
            {
                ftpClient = new FTP.FTPclient(server, user, password);

                if (fileName.Contains("[date]"))
                {
                    fileName = fileName.Replace("[date]", DateTime.Today.ToString("yyyyMMdd"));
                }

                if (ftpClient.FtpFileExists(fileName))
                {
                    return new Result(ActionName, Result.ResultValue.Pass, String.Format("Found file {0} on server {1}", fileName, server));
                }
                else
                {
                    return new Result(ActionName, Result.ResultValue.Fail, String.Format("Didn't find file {0} on server {1}!", fileName, server));
                }
            }
            catch (WebException exc)
            {
                return new Result(ActionName, Result.ResultValue.Fail, "Exception encountered: " + exc.ToString());
            }
            finally
            {
                ftpClient = null;
            }
        }

        protected string fileName;

        // Name of the action - used by notifiers
        protected override string ActionName
        {
            get { return "FTP File Check - " + server + ": " + fileName; }
        }
    }
}
