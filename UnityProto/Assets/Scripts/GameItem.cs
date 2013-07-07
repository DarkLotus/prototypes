using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class GameItem
    {
        //Game Design values
        public String Name;
        public int Genre = 0xFFF; //Default value 0xFFF instead of 0 because array of genres starts at 0;
        public float FeelFactor;
        public int Rating;
        public int SubGenre = 0xFFF;
        public List<GameFeature> Features = new List<GameFeature>();
        public GameScope Scope;

        //Sales data
        public int RlsMonth, RlsYear;
        public int Cost, Sales, Price;
        
    }


    public enum GameScope{
        Small,
        Medium,
        Large,
        Massive

    }

    

}
