using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace PokeMon
{
    class PokeMbUnitTestTask : ITask
    {
        public PokeMbUnitTestTask(string dllName, string categories, ParameterCollection parameters, string reportDirectory)
        {
            _dllName = dllName;
            _categories = categories;
            _parameters = parameters;
            _reportDirectory = reportDirectory;

            PrepareReportDirectory(_reportDirectory);
        }

        private void PrepareReportDirectory(string reportDirectory)
        {
            if (!String.IsNullOrEmpty(reportDirectory) && !Directory.Exists(reportDirectory))
            {
                Directory.CreateDirectory(reportDirectory);
            }
        }

        public Result Perform()
        {
            Process mbUnitRunner = BuildRunnerProcess();
            AddEnvironmentVariables(mbUnitRunner);

            mbUnitRunner.Start();

            if (!mbUnitRunner.WaitForExit(_maxWaitTime))
            {
                return new Result(ActionName, Result.ResultValue.Fail,
                    String.Format("Tests didn't finish running during max time allotted: {0} miliseconds. Ran {1} {2}.",
                        _maxWaitTime,
                        mbUnitRunner.StartInfo.FileName,
                        mbUnitRunner.StartInfo.Arguments));
            }

            if (mbUnitRunner.ExitCode == 0)
            {
                string output = mbUnitRunner.StandardOutput.ReadToEnd();
                Regex regex = new Regex(@"all\s*?tests\s*?finished:\s*?(?<totalTests>\d+)\s*?tests", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                Match match = regex.Match(output);

                if (!match.Success)
                {
                    return new Result(ActionName, Result.ResultValue.Fail,
                        String.Format("Couldn't determine test statistics. Ran {0} {1}.",
                            mbUnitRunner.StartInfo.FileName,
                            mbUnitRunner.StartInfo.Arguments));
                }

                if (match.Groups["totalTests"].Value == "0")
                {
                    return new Result(ActionName, Result.ResultValue.Warning,
                        String.Format("No tests found.",
                            mbUnitRunner.StartInfo.FileName,
                            mbUnitRunner.StartInfo.Arguments));
                }

                return new Result(ActionName, Result.ResultValue.Pass, "All tests passed.");
            }
            else
            {
                return new Result(ActionName, Result.ResultValue.Fail,
                    String.Format("One or more tests failed with exit code: {0}. Ran {1} {2}.",
                        mbUnitRunner.ExitCode,
                        mbUnitRunner.StartInfo.FileName,
                        mbUnitRunner.StartInfo.Arguments));
            }
        }

        protected Process BuildRunnerProcess()
        {
            Process mbUnitRunner = new Process();
            mbUnitRunner.EnableRaisingEvents = false;
            mbUnitRunner.StartInfo.UseShellExecute = false;

            mbUnitRunner.StartInfo.RedirectStandardError = true;
            mbUnitRunner.StartInfo.RedirectStandardOutput = true;

            string programFilesPath = Environment.GetEnvironmentVariable("ProgramFiles");

            mbUnitRunner.StartInfo.WorkingDirectory = programFilesPath + @"\MbUnit\bin\";
            mbUnitRunner.StartInfo.FileName = mbUnitRunner.StartInfo.WorkingDirectory + "MbUnit.Cons.exe";
            mbUnitRunner.StartInfo.Arguments = String.Format("\"{0}\" /report-type:html /report-folder:\"{1}\"", _dllName, _reportDirectory);

            if (!String.IsNullOrEmpty(_categories))
            {
                mbUnitRunner.StartInfo.Arguments += " /fc:" + _categories;
            }

            return mbUnitRunner;
        }

        protected void AddEnvironmentVariables(Process mbUnitRunner)
        {
            foreach (ParameterSettings parameter in _parameters)
            {
                mbUnitRunner.StartInfo.EnvironmentVariables.Add(parameter.Name, parameter.Value);
            }
        }

        protected string _dllName;
        protected string _categories;
        protected ParameterCollection _parameters = null;
        protected string _reportDirectory;

        protected const int _maxWaitTime = 5 * 60000;  // 5 minutes

        // Name of the action - used by notifiers
        protected virtual string ActionName
        {
            get { return "MbUnit Test - " + _dllName + " (Categories: " + _categories + ")"; }
        }
    }
}
