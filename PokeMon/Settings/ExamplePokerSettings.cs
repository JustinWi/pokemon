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
    class ExamplePokerSettings : ActionSettings
    {
        // Property used to access the attribute in the config file
        [ConfigurationProperty(ExampleAttributeName, IsRequired = true)]
        [StringValidator(InvalidCharacters = "@~!#^*()[]{};'\"|\\ ", MinLength = 0, MaxLength = 256)]
        public string ExampleParameter
        {
            get
            {
                return (string)this[ExampleAttributeName];
            }
            set
            {
                this[ExampleAttributeName] = value;
            }
        }

        // String used to identify the parameter
        protected const string ExampleAttributeName = "exampleParameter";
    }
}
