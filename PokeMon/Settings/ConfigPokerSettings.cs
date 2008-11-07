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
    class ConfigPokerSettings : ActionSettings
    {
        // Property used to access the attribute in the config file
        [ConfigurationProperty(configPathAttribute, IsRequired = true)]
        [StringValidator(InvalidCharacters = "@~!#^*()[]{};'\"|", MinLength = 0, MaxLength = 256)]
        public string ConfigPath
        {
            get
            {
                return (string)this[configPathAttribute];
            }
            set
            {
                this[configPathAttribute] = value;
            }
        }

        // String used to identify the parameter
        protected const string configPathAttribute = "filePath";


    }
}
