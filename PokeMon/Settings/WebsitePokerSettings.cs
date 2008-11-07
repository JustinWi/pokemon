using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace PokeMon
{
    class WebsitePokerSettings : ActionSettings
    {
        [ConfigurationProperty(URIAttributeName, IsRequired = true)]
        [StringValidator(InvalidCharacters = "@~!#^*()[]{};'\"|\\ ", MinLength = 0, MaxLength = 256)]
        public string URI
        {
            get
            {
                return (string)this[URIAttributeName];
            }
            set
            {
                this[URIAttributeName] = value;
            }
        }

        [ConfigurationProperty(FindStringAttributeName, DefaultValue = "body", IsRequired = false)]
        [StringValidator(MinLength = 1, MaxLength = 256)]
        public string FindString
        {
            get
            {
                return (string)this[FindStringAttributeName];
            }
            set
            {
                this[FindStringAttributeName] = value;
            }
        }

        protected const string URIAttributeName = "uri";
        protected const string FindStringAttributeName = "verifyPageContains";
    }
}
