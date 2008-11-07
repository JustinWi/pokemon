using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace PokeMon
{
    class PokeConfigTask : ITask
    {
        public PokeConfigTask(string configName)
        {
            this.configName = configName;
        }

        public Result Perform()
        {
            StreamReader reader = null;

            try
            {
                reader = new StreamReader(configName);
            }
            catch (FileNotFoundException exc)
            {
                reader.Close();
                return new Result(ActionName, Result.ResultValue.Fail, exc.Message);
            }

            foreach (string badWord in badWords)
            {
                if (reader.ReadToEnd().ToLower().Contains(badWord))
                {
                    reader.Close();
                    return new Result(ActionName, Result.ResultValue.Fail, "Found bad word in config file: " + badWord + ".  File may not be encrypted.");
                }
            }

            reader.Close();
            return new Result(ActionName, Result.ResultValue.Pass, "Can't find any bad words.");
        }

        protected string configName;
        protected string[] badWords = new string[] { "password", "username", "pwd", "uid" };

        // Name of the action - used by notifiers
        protected virtual string ActionName
        {
            get { return "Poke Config File - " + configName; }
        }
    }
}
