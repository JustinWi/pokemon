using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PokeMon
{
    public class PokeMonHeartbeat : IPokeMonHeartbeat
    {
        public bool AreYouAlive()
        {
            return true;
        }
    }
}
