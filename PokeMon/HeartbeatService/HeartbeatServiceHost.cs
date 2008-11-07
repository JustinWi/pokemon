using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace PokeMon
{
    internal class HeartbeatServiceHost
    {
        internal static ServiceHost heartbeatServiceHost = null;

        internal static void StartService(string baseAddress)
        {
            // Instantiate new ServiceHost 
            heartbeatServiceHost = new ServiceHost(typeof(PokeMon.HeartbeatService), new Uri(baseAddress));

            // Open breezeServiceHost
            heartbeatServiceHost.Open();
        }

        internal static void StopService()
        {
            //Call StopService from your shutdown logic (i.e. dispose method)
            if (heartbeatServiceHost.State != CommunicationState.Closed)
            {
                heartbeatServiceHost.Close();
            }
        }
    }
}