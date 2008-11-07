using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace PokeMon
{
    class PokeExampleTask : ITask
    {
        public PokeExampleTask(string exampleParameter)
        {
            this.exampleParameter = exampleParameter;
        }

        public Result Perform()
        {
            // Perform example tasks
            int i = 5;

            if (i > 0)
            {
                return new Result(ActionName, Result.ResultValue.Pass, "Everything worked perfectly!");
            }
            else
            {
                return new Result(ActionName, Result.ResultValue.Fail, "Nothing worked!");
            }
        }

        protected string exampleParameter;

        // Name of the action - used by notifiers
        protected virtual string ActionName
        {
            get { return "Example action - " + exampleParameter; }
        }
    }
}
