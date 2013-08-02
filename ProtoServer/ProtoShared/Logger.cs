using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ProtoShared
{
    public static class Logger
    {
        public static void Log(string text) {
            //TODO unity output here
            Console.WriteLine("Debug- " + text);
        }
    }
}
