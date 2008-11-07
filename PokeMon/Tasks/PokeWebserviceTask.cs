using System;
using System.Collections.Generic;
using System.Text;

namespace PokeMon
{
    class PokeWebserviceTask : PokeWebsiteTask
    {
        // TODO: Current this task just relies on the same mechanisms poking a website does
        // to determine if the webserice is available.  This requires the webservice to
        // return an HTTP response when accessed via a web client - a minor security risk.
        // Should implement a version of webserice poking that doesn't require this.

        public PokeWebserviceTask(string uri, string stringToFind) : base(uri, stringToFind)
        {
        }

        protected override string ActionName
        {
            get { return "Webservice Monitor - " + uri; }
        }
    }
}
