using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PokeMon
{
    class Driver
    {
        public Driver()
        {
            List<Notifier> globalNotifiers = GetNotifiers(MonitorSettings.GlobalNotificationSettings);

            actions = InitializeActions(globalNotifiers);
        }

        public void Start()
        {
            StartHeartbeatService();
            StartActions();
        }

        private void StartHeartbeatService()
        {
            // Start our heartbeat service
            HeartbeatServiceHost.StartService(MonitorSettings.HeartbeatServiceSettings.ServiceAddress);
        }

        private void StartActions()
        {
            Console.WriteLine("Found {0} action(s).", actions.Count);

            // Start timers for each of the actions
            for (int ndx = 0; ndx < actions.Count; ndx++)
            {
                Action action = actions[ndx];

                Console.WriteLine("Starting action {0} of {1}...", ndx + 1, actions.Count);
                action.Timer = new Timer(new TimerCallback(action.TakeAction), null, new TimeSpan(0), action.TaskInterval);

                // Sleeping in between each action start so that we don't hammer the system by starting them all at the same time.
                Thread.Sleep(1000);                
            }

            Console.WriteLine("All actions started successfully.", actions.Count);
        }

        public void Stop()
        {
            StopHeartbeatService();
            StopActions();
        }

        private void StopHeartbeatService()
        {
            // Stop our heartbeat service
            HeartbeatServiceHost.StopService();
        }

        private void StopActions()
        {
            // Clean up each of the timers
            foreach (Action action in actions)
            {
                action.Timer.Dispose();
            }
        }

        private static List<Action> InitializeActions(List<Notifier> globalNotifiers)
        {
            List<Action> actions = new List<Action>();

            #region Website Pokers
            // Get all of the website poker settings and create actions out of them
            foreach (WebsitePokerSettings actionSetting in MonitorSettings.WebsitePokers)
            {
                // Get the notifiers specific to this action
                List<Notifier> actionNotifiers = GetNotifiers(actionSetting);

                // Add the global notifiers
                actionNotifiers.AddRange(globalNotifiers);

                // Create a new action and add it to our collection
                actions.Add(new Action(new PokeWebsiteTask(actionSetting.URI, actionSetting.FindString), actionSetting.Interval, actionNotifiers));
            }
            #endregion

            #region Webservice Pokers
            // Get all of the webservice poker settings and create actions out of them
            foreach (WebservicePokerSettings actionSetting in MonitorSettings.WebservicePokers)
            {
                // Get the notifiers specific to this action
                List<Notifier> actionNotifiers = GetNotifiers(actionSetting);

                // Add the global notifiers
                actionNotifiers.AddRange(globalNotifiers);

                // Create a new action and add it to our collection
                actions.Add(new Action(new PokeWebserviceTask(actionSetting.URI, actionSetting.FindString), actionSetting.Interval, actionNotifiers));
            }
            #endregion

            #region Config Pokers
            // Get all of the config file poker settings and create actions out of them
            foreach (ConfigPokerSettings actionSetting in MonitorSettings.ConfigPokers)
            {
                // Get the notifiers specific to this action
                List<Notifier> actionNotifiers = GetNotifiers(actionSetting);

                // Add the global notifiers
                actionNotifiers.AddRange(globalNotifiers);

                // Create a new action and add it to our collection
                actions.Add(new Action(new PokeConfigTask(actionSetting.ConfigPath), actionSetting.Interval, actionNotifiers));
            }
            #endregion

            #region MbUnit Poker
            // Get all of the MbUnit poker settings and create actions out of them
            foreach (MbUnitPokerSettings actionSetting in MonitorSettings.MbUnitPokers)
            {
                // Get the notifiers specific to this action
                List<Notifier> actionNotifiers = GetNotifiers(actionSetting);

                // Add the global notifiers
                actionNotifiers.AddRange(globalNotifiers);

                // Create a new action and add it to our collection
                actions.Add(new Action(new PokeMbUnitTestTask(actionSetting.TestDll, actionSetting.Categories, actionSetting.ParameterCollection, actionSetting.ReportDirectory), actionSetting.Interval, actionNotifiers));
            }
            #endregion

            #region FTP Audit Pokers
            // Get all of the webservice poker settings and create actions out of them
            foreach (FtpAuditPokerSettings actionSetting in MonitorSettings.FtpAuditPokers)
            {
                // Get the notifiers specific to this action
                List<Notifier> actionNotifiers = GetNotifiers(actionSetting);

                // Add the global notifiers
                actionNotifiers.AddRange(globalNotifiers);

                // Create a new action and add it to our collection
                actions.Add(new Action(new PokeFtpAuditTask(actionSetting.ServerURI, 
                                                            actionSetting.Username, 
                                                            actionSetting.Password, 
                                                            actionSetting.DiskSpaceWarningLevel, 
                                                            actionSetting.DiskSpaceFailureLevel), actionSetting.Interval, actionNotifiers));
            }
            #endregion

            #region FTP File Pokers
            // Get all of the webservice poker settings and create actions out of them
            foreach (FtpFilePokerSettings actionSetting in MonitorSettings.FtpFilePokers)
            {
                // Get the notifiers specific to this action
                List<Notifier> actionNotifiers = GetNotifiers(actionSetting);

                // Add the global notifiers
                actionNotifiers.AddRange(globalNotifiers);

                // Create a new action and add it to our collection
                actions.Add(new Action(new PokeFtpFileTask(actionSetting.ServerURI, actionSetting.Username, actionSetting.Password, actionSetting.FileName), actionSetting.Interval, actionNotifiers));
            }
            #endregion

            return actions;
        }

        private static List<Notifier> GetNotifiers(NotifiableConfigurationElement notifiable)
        {
            List<Notifier> notifiers = new List<Notifier>();

            // Add email notifiers
            foreach (NotificationSettings notifierSettings in notifiable.EmailNotificationSettingsCollection)
            {
                notifiers.Add(new EmailNotifier(notifierSettings.Destination, notifierSettings.Threshold));
            }

            // Add SMS notifiers
            foreach (NotificationSettings notifierSettings in notifiable.SMSNotificationsSettingsCollection)
            {
                notifiers.Add(new SMSNotifier(notifierSettings.Destination, notifierSettings.Threshold));
            }

            // Add console notifiers
            foreach (NotificationSettings notifierSettings in notifiable.ConsoleNotificationSettingsCollection)
            {
                notifiers.Add(new ConsoleNotifier(notifierSettings.Threshold));
            }

            return notifiers;
        }

        private List<Action> actions;
    }
}
