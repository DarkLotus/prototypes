using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
   public static class GameValues
    {
       public static string[] genreStrings = new string[] { "Fighting", "Maze", "Pinball", "Platformer", "FPS", "Third-Person Shooter", "Action", "Adventure", "RPG", "Simulation", "RTS", "Turn-based Strategy", "Casual", "Music", "Sports", "Trivia", "Board", "Card" };
       public static string[] GameScope = new string[] { "Small","Medium","Large","Massive" };


       public static List<GameFeature> AvailableEngFeatures { get; set; }

       static GameValues() {
           AvailableEngFeatures = new List<GameFeature>();
           AvailableEngFeatures.Add(new GameFeature("Text Based", "Simplistic text based engine",0,0));
           AvailableEngFeatures.Add(new GameFeature("8 bit Sprite", "Simple 8 bit sprite graphic engine", -0.1f, 0.1f));
           AvailableEngFeatures.Add(new GameFeature("Isometric Sprite", "Isometric Engine", -0.2f, 0.1f));
           AvailableEngFeatures.Add(new GameFeature("Generic 2D", "Simplistic versatile 2D engine", -0.1f, 0.1f));
           AvailableEngFeatures.Add(new GameFeature("Online Only DRM", "Stop pirates in their tracks", -0.1f, -0.5f));
       }
    }
}
