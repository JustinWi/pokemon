using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PokeMon
{
    class Program
    {
        static void Main(string[] args)
        {
            Driver driver = new Driver();
            string input = "";

            driver.Start();

            // Keep running until the user wants to quit
            while (input.ToLower() != quitStr.ToLower())
            {
                Console.WriteLine("Type \"" + quitStr + "\" to exit.");
                input = Console.ReadLine();
            }

            driver.Stop();
        }

        private const string quitStr = "quit";
    }
}
