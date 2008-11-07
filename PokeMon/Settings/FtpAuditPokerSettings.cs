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
    class FtpAuditPokerSettings : ActionSettings
    {
        // Property used to access the attribute in the config file
        [ConfigurationProperty(ServerAttributeName, IsRequired = true)]
        [StringValidator(InvalidCharacters = "@~!#^*()[]{};'\"|\\ ", MinLength = 0, MaxLength = 256)]
        public string ServerURI
        {
            get
            {
                return (string)this[ServerAttributeName];
            }
            set
            {
                this[ServerAttributeName] = value;
            }
        }

        [ConfigurationProperty(UserAttributeName, IsRequired = true)]
        [StringValidator(InvalidCharacters = "@~!#^*()[]{};'\"|\\ ", MinLength = 0, MaxLength = 256)]
        public string Username
        {
            get
            {
                return (string)this[UserAttributeName];
            }
            set
            {
                this[UserAttributeName] = value;
            }
        }

        [ConfigurationProperty(PasswordAttributeName, IsRequired = true)]
        [StringValidator(MinLength = 0, MaxLength = 256)]
        public string Password
        {
            get
            {
                return (string)this[PasswordAttributeName];
            }
            set
            {
                this[PasswordAttributeName] = value;
            }
        }

        [ConfigurationProperty(DiskSpaceWarningAttributeName, DefaultValue = long.MaxValue, IsRequired = false)]
        public long DiskSpaceWarningLevel
        {
            get
            {
                return (long)this[DiskSpaceWarningAttributeName];
            }
            set
            {
                this[DiskSpaceWarningAttributeName] = value;
            }
        }

        [ConfigurationProperty(DiskSpaceFailureAttributeName, DefaultValue = long.MaxValue, IsRequired = false)]
        public long DiskSpaceFailureLevel
        {
            get
            {
                return (long)this[DiskSpaceFailureAttributeName];
            }
            set
            {
                this[DiskSpaceFailureAttributeName] = value;
            }
        }

        // String used to identify the parameter
        protected const string ServerAttributeName = "server";
        protected const string UserAttributeName = "username";
        protected const string PasswordAttributeName = "password";
        protected const string DiskSpaceWarningAttributeName = "diskSpaceWarningMB";
        protected const string DiskSpaceFailureAttributeName = "diskSpaceFailureMB";
    }


}
