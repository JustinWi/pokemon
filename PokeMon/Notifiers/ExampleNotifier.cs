using System;
using System.Collections.Generic;
using System.Text;

namespace PokeMon
{
    // Derived from Notifier class which does the heavy lifting.  Note, if the properties of Notifier
    // aren't sufficient, you'll want to add them here and then add them to a child class of 
    // NotificationSettings.
    class ExampleNotifier : Notifier
    {
        public ExampleNotifier(Result.ResultValue threshold)
            : base(null, threshold)
        {
        }

        public override void Notify(Result message)
        {
            // Perform notification using the information in the result.  
            // Will only be invoked when the result value meets the threshold so there's no need to check that.

            Console.WriteLine(message.ToString());
        }
    }
}
