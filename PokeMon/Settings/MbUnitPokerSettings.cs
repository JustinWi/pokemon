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
    class MbUnitPokerSettings : ActionSettings
    {
        // Property used to access the attribute in the config file
        [ConfigurationProperty(testDllAttributeName, IsRequired = true)]
        [StringValidator(InvalidCharacters = "@!#^*()[]{};'\"|", MinLength = 0, MaxLength = 256)]
        public string TestDll
        {
            get
            {
                return (string)this[testDllAttributeName];
            }
            set
            {
                this[testDllAttributeName] = value;
            }
        }

        [ConfigurationProperty(categoryFilterAttributeName, IsRequired = false)]
        [StringValidator(InvalidCharacters = "@~!#^*()[]{};'\"|\\ ", MinLength = 0, MaxLength = 256)]
        public string Categories
        {
            get
            {
                return (string)this[categoryFilterAttributeName];
            }
            set
            {
                this[categoryFilterAttributeName] = value;
            }
        }

        [ConfigurationProperty(reportDirectoryAttributeName, IsRequired = false)]
        [StringValidator(InvalidCharacters = "@!#^*{};'\"|", MinLength = 0, MaxLength = 256)]
        public string ReportDirectory
        {
            get
            {
                return (string)this[reportDirectoryAttributeName];
            }
            set
            {
                this[reportDirectoryAttributeName] = value;
            }
        }

        // String used to identify the parameter
        protected const string testDllAttributeName = "testDllPath";
        protected const string categoryFilterAttributeName = "categories";
        protected const string reportDirectoryAttributeName = "reportDirectoryFullPath";
    }
}
