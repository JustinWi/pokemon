using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace PokeMon
{
    public class ParameterSettings : ConfigurationElement
    {
        [ConfigurationProperty(NameAttributeName, IsRequired = true)]
        [StringValidator(InvalidCharacters = "@.~!#$%^&*()[]{}/;'\"|\\", MinLength = 0, MaxLength = 128)]
        public string Name
        {
            get { return (string)this[NameAttributeName]; }
            set { this[NameAttributeName] = value; }
        }

        [ConfigurationProperty(ValueAttributeName, IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!#$^*'\"|", MinLength = 0, MaxLength = 128)]
        public string Value
        {
            get { return (string)this[ValueAttributeName]; }
            set { this[ValueAttributeName] = value; }
        }

        private const string NameAttributeName = "name";
        private const string ValueAttributeName = "value";
    }
}
