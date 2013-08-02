using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtoServer
{
    public static class IDFactory
    {
        //TODO save in DB
        private static int _nextID = 1;
        public static int getNextAvailableID(){
            return _nextID++;
        }
    }
}
