using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace PokeMon
{
    public class NotificationSettings : ConfigurationElement
    {
        [ConfigurationProperty(DestinationAttributeName, DefaultValue = "")]
        [StringValidator(InvalidCharacters = "~!#$%^&*,[]{}/'\"|\\")]
        public string Destination
        {
            get
            {
                return (string)this[DestinationAttributeName];
            }
            set
            {
                this[DestinationAttributeName] = value;
            }
        }

        /// <summary>
        /// Need this in addition to the private threshold property because the configurationProperty
        /// needs to be a string and this property should be a ResultValue.
        /// </summary>
        public Result.ResultValue Threshold
        {
            get 
            {
                // convert the string in the config file to an enum
                return (Result.ResultValue)Enum.Parse(typeof(Result.ResultValue), threshold, true);
            }
        }

        [ConfigurationProperty(ThresholdAttributeName, DefaultValue = "Fail", IsRequired = false)]
        [StringValidator(InvalidCharacters = "@.~!#$%^&*()[]{}/;'\"|\\", MinLength = 4, MaxLength = 7)]
        private string threshold
        {
            get { return (string)this[ThresholdAttributeName]; }
            set { this[ThresholdAttributeName] = value; }
        }

        private const string DestinationAttributeName = "destination";
        private const string ThresholdAttributeName = "resultThreshold";
    }

    public class NotificationCollection : ConfigurationElementCollection
    {
        public NotificationCollection this[int index]
        {
            get
            {
                return base.BaseGet(index) as NotificationCollection;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
         
            return new NotificationSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((NotificationSettings)element).Destination;
        }
    } 
}

