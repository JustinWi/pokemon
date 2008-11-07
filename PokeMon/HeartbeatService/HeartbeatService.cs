using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PokeMon
{
    public class HeartbeatService : IHeartbeatService
    {
        public bool AreYouAlive()
        {
            return true;
        }
    }
}
