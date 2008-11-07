using System;
using System.Collections.Generic;
using System.Text;

namespace PokeMon
{
    abstract class Notifier
    {
        public Notifier(string audience) : this(audience, Result.ResultValue.Fail)
        {
        }

        public Notifier(string audience, Result.ResultValue threshold)
        {
            this.audience = audience;
            this.threshold = threshold;
        }

        public bool MeetsThreshold(Result.ResultValue result)
        {
            return result >= threshold;
        }

        public abstract void Notify(Result message);

        private string audience;
        public virtual string Audience
        {
            get { return audience; }
        }

        private Result.ResultValue threshold;
        public Result.ResultValue Threshold
        {
            get { return threshold; }
        }
    }
}
