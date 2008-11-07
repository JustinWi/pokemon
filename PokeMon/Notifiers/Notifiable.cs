using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace PokeMon
{
    public class NotifiableConfigurationElement : ConfigurationElement
    {
        #region Notifiers
        [ConfigurationProperty(EmailSettingsSectionName)]
        public NotificationCollection EmailNotificationSettingsCollection
        {
            get
            {
                return this[EmailSettingsSectionName] as NotificationCollection;
            }
        }

        [ConfigurationProperty(SMSSettingsSectionName)]
        public NotificationCollection SMSNotificationsSettingsCollection
        {
            get
            {
                return this[SMSSettingsSectionName] as NotificationCollection;
            }
        }

        [ConfigurationProperty(ConsoleSettingsSectionName)]
        public NotificationCollection ConsoleNotificationSettingsCollection
        {
            get
            {
                return this[ConsoleSettingsSectionName] as NotificationCollection;
            }
        }
        #endregion

        #region Parameters
        [ConfigurationProperty(ParametersSectionName)]
        public ParameterCollection ParameterCollection
        {
            get
            {
                return this[ParametersSectionName] as ParameterCollection;
            }
        }
        #endregion

        protected const string EmailSettingsSectionName = "emailNotification";
        protected const string SMSSettingsSectionName = "SMSNotification";
        protected const string ConsoleSettingsSectionName = "consoleNotification";

        protected const string ParametersSectionName = "environmentVariables";
    }
}
