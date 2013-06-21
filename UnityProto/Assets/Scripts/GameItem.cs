using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class GameItem
    {
        public String Name;
        public int Cost, Profit;

        public Genre Genre;
        public int RlsMonth, RlsYear;
        
    }


    public enum GameAtmosphere{
    Dark, Upbeat, Childish

    }
    public enum Genre{
    RTS,
        FPS,
        TCG,
        Driving,
        Simulation
    }

}
