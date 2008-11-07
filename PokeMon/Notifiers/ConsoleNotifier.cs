using System;
using System.Collections.Generic;
using System.Text;

namespace PokeMon
{
    class ConsoleNotifier : Notifier
    {
        public ConsoleNotifier(Result.ResultValue threshold)
            : base(null, threshold)
        {
        }

        public override void Notify(Result message)
        {
            Console.WriteLine(message.ToString());
        }
    }
}
