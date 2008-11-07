using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace PokeMon
{
    class MonitorSettings : ConfigurationSection
    {
        #region Website Pokers
        public static WebsitePokerCollection WebsitePokers
        {
            get { return settings.websitePokers; }
        }

        [ConfigurationProperty(PokeWebsiteSectionName)]
        private WebsitePokerCollection websitePokers
        {
            get { return this[PokeWebsiteSectionName] as WebsitePokerCollection; }
        }
        #endregion

        #region Webservice Pokers
        public static WebservicePokerCollection WebservicePokers
        {
            get { return settings.webservicePokers; }
        }

        [ConfigurationProperty(PokeWebserviceSectionName)]
        private WebservicePokerCollection webservicePokers
        {
            get { return this[PokeWebserviceSectionName] as WebservicePokerCollection; }
        }
        #endregion

        #region Config File Pokers
        public static ConfigPokerCollection ConfigPokers
        {
            get { return settings.configFilePokers; }
        }

        [ConfigurationProperty(PokeConfigFileSectionName)]
        private ConfigPokerCollection configFilePokers
        {
            get { return this[PokeConfigFileSectionName] as ConfigPokerCollection; }
        }
        #endregion

        #region FTP Audit Pokers
        public static FtpAuditPokerCollection FtpAuditPokers
        {
            get { return settings.ftpAuditPokers; }
        }

        [ConfigurationProperty(PokeFtpAuditSectionName)]
        private FtpAuditPokerCollection ftpAuditPokers
        {
            get { return this[PokeFtpAuditSectionName] as FtpAuditPokerCollection; }
        }
        #endregion

        #region FTP File Pokers
        public static FtpFilePokerCollection FtpFilePokers
        {
            get { return settings.ftpFilePokers; }
        }

        [ConfigurationProperty(PokeFtpFileSectionName)]
        private FtpFilePokerCollection ftpFilePokers
        {
            get { return this[PokeFtpFileSectionName] as FtpFilePokerCollection; }
        }
        #endregion

        #region MbUnit Pokers
        public static MbUnitPokerCollection MbUnitPokers
        {
            get { return settings.mbUnitPokers; }
        }

        [ConfigurationProperty(MbUnitSectionName)]
        private MbUnitPokerCollection mbUnitPokers
        {
            get { return this[MbUnitSectionName] as MbUnitPokerCollection; }
        }
        #endregion

        #region Example Pokers
        public static ExamplePokerCollection ExamplePokers
        {
            get { return settings.examplePokers; }
        }

        [ConfigurationProperty(PokeExampleSectionName)]
        private ExamplePokerCollection examplePokers
        {
            get { return this[PokeExampleSectionName] as ExamplePokerCollection; }
        }
        #endregion

        #region Global Notifiaction Settings
        public static NotifiableConfigurationElement GlobalNotificationSettings
        {
            get { return settings.globalNotificationSettings; }
        }

        [ConfigurationProperty(GlobalNotifiersSectionName)]
        private NotifiableConfigurationElement globalNotificationSettings
        {
            get { return this[GlobalNotifiersSectionName] as NotifiableConfigurationElement; }
        }
        #endregion

        #region Heartbeat Settings
        public static HeartbeatServiceSettings HeartbeatServiceSettings
        {
            get { return settings.heartbeatServiceSettings; }
        }

        [ConfigurationProperty(HeartbeatSectionName)]
        private HeartbeatServiceSettings heartbeatServiceSettings
        {
            get { return this[HeartbeatSectionName] as HeartbeatServiceSettings; }
        }
        #endregion 

        private const string SectionName = "pokeMon";
        private const string HeartbeatSectionName = "heartbeatService";
        private const string GlobalNotifiersSectionName = "globalNotifiers";

        private const string PokeWebsiteSectionName = "websites";
        private const string PokeWebserviceSectionName = "webservices";
        private const string PokeExampleSectionName = "examplePokers";
        private const string PokeFtpAuditSectionName = "ftpAudits";
        private const string PokeFtpFileSectionName = "ftpFiles";
        private const string PokeConfigFileSectionName = "cnfgFiles";
        private const string MbUnitSectionName = "mbUnitTests";
        
        private static MonitorSettings settings = ConfigurationManager.GetSection(SectionName) as MonitorSettings;
    }
}
