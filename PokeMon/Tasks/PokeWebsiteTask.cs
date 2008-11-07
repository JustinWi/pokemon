using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace PokeMon
{
    class PokeWebsiteTask : ITask
    {
        public PokeWebsiteTask(string uri, string stringToFind)
        {
            this.uri = uri;
            this.stringToFind = stringToFind;
        }

        public Result Perform()
        {
            WebClient webClient = new WebClient();

            try
            {
                string page = webClient.DownloadString(uri);

                if (page.ToUpper().Contains(stringToFind.ToUpper()))
                {
                    // Found what we were looking for
                    return new Result(ActionName, Result.ResultValue.Pass, "Found string \"" + stringToFind + "\" in web request to " + uri + ".");
                }
                else
                {
                    // Didn't find what we were looking for
                    return new Result(ActionName, Result.ResultValue.Fail, "Couldn't find the string \"" + stringToFind + "\" in the web request to " + uri + ".");
                }
            }
            catch (WebException exc)
            {
                // Couldn't connect to the website
                return new Result(ActionName, Result.ResultValue.Fail, "Couldn't connect to " + uri + ". Got exception: " + exc.Message);
            }
        }

        protected string uri;
        protected string stringToFind;

        protected virtual string ActionName
        {
            get { return "Website Monitor - " + uri; }
        }
    }
}
