using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;

namespace PokeMon
{
    public class ActionSettings : NotifiableConfigurationElement
    {
        public TimeSpan Interval
        {
            get { return new TimeSpan(intervalHours, intervalMinutes, intervalSeconds); }
        }

        [ConfigurationProperty(SecondsIntervalName, DefaultValue = "0", IsRequired = false)]
        [IntegerValidator(MinValue = 0)]
        protected int intervalSeconds
        {
            get
            {
                return (int)this[SecondsIntervalName];
            }
            set
            {
                this[SecondsIntervalName] = value;
            }
        }

        [ConfigurationProperty(MinutesIntervalName, DefaultValue = "0", IsRequired = false)]
        [IntegerValidator(MinValue = 0)]
        protected int intervalMinutes
        {
            get
            {
                return (int)this[MinutesIntervalName];
            }
            set
            {
                this[MinutesIntervalName] = value;
            }
        }

        [ConfigurationProperty(HoursIntervalName, DefaultValue = "0", IsRequired = false)]
        [IntegerValidator(MinValue = 0)]
        protected int intervalHours
        {
            get
            {
                return (int)this[HoursIntervalName];
            }
            set
            {
                this[HoursIntervalName] = value;
            }
        }

        private const string SecondsIntervalName = "intervalSeconds";
        private const string MinutesIntervalName = "intervalMinutes";
        private const string HoursIntervalName = "intervalHours";
    }
}
