using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ProtoServer
{
    public static class Logger
    {
        public static void Log(string text) {
            Console.WriteLine("Debug- " + text);
        }
    }
}
