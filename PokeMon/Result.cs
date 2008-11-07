using System;
using System.Collections.Generic;
using System.Text;

namespace PokeMon
{
    public class Result
    {
        public Result(string actionName, ResultValue value) : this(actionName, value, "")
        {
        }

        public Result(string actionName, ResultValue value, string description)
        {
            this.value = value;
            this.description = description;
            this.actionName = actionName;

            time = DateTime.Now;
        }

        public override string ToString()
        {
            return Time.ToString() + " - " + value.ToString() + " - " + ActionName + ": " + description;
        }

        private String actionName;
        public String ActionName
        {
            get { return actionName; }
        }

        private String description;
        public String Description
        {
            get { return description; }
        }

        private DateTime time;
        public DateTime Time
        {
            get { return time; }
        }

        private ResultValue value;
        public ResultValue Value
        {
            get { return value; }
        }
        
        public enum ResultValue
        {
            Pass,
            Warning,
            Fail
        };
    }
}
