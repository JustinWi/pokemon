using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace PokeMon
{
    /// <summary>
    /// Represents the collection of action attributes specified in the config file.  Used to convert
    /// those attributes into action properties.
    /// </summary>
    class HeartbeatServiceSettings : ConfigurationElement
    {
        // Property used to access the attribute in the config file
        [ConfigurationProperty(serviceAddressAttributeName, IsRequired = true)]
        [StringValidator(InvalidCharacters = "@~!#^*()[]{};'\"|\\ ", MinLength = 0, MaxLength = 256)]
        public string ServiceAddress
        {
            get
            {
                return (string)this[serviceAddressAttributeName];
            }
            set
            {
                this[serviceAddressAttributeName] = value;
            }
        }

        // String used to identify the parameter
        protected const string serviceAddressAttributeName = "serviceAddress";
    }
}
