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
    class FtpFilePokerSettings : FtpAuditPokerSettings
    {
        // Property used to access the attribute in the config file
        [ConfigurationProperty(FileNameAttributeName, IsRequired = true)]
        [StringValidator(InvalidCharacters = "@~!#^*{};'\"|\\ ", MinLength = 0, MaxLength = 256)]
        public string FileName
        {
            get
            {
                return (string)this[FileNameAttributeName];
            }
            set
            {
                this[FileNameAttributeName] = value;
            }
        }

        // String used to identify the parameter
        protected const string FileNameAttributeName = "file";
    }


}
