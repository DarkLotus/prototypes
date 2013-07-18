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


       public static List<GameFeature> Features { get; set; }
       public static List<Training> Training { get; set; }
       public static List<Skill> Skill { get; set; }

       public static Skill Programming = new Skill(1, "Programming", "The art of code", 0);
       public static Skill Art = new Skill(2,"Art", "The art of art", 0);
       static GameValues() {
           Features = new List<GameFeature>();
           Features.Add(new GameFeature("Text Based", "Simplistic text based engine",0,0));
           Features.Add(new GameFeature("8 bit Sprite", "Simple 8 bit sprite graphic engine", -0.1f, 0.1f));
           Features.Add(new GameFeature("Isometric Sprite", "Isometric Engine", -0.2f, 0.1f));
           Features.Add(new GameFeature("Generic 2D", "Simplistic versatile 2D engine", -0.1f, 0.1f));
           Features.Add(new GameFeature("Online Only DRM", "Stop pirates in their tracks", -0.1f, -0.5f));

           Training = new List<Training>();
           Training.Add(new Training("Programming Ebook", "Cheap Ebook", Programming, 500,5));
           Training.Add(new Training("Art Ebook", "Cheap Ebook", Art, 500,5));
       }
    }

   public class Training
   {
       public string Name, Description;
       public Skill Skill;
       public int Price,Strength;


       public Training(string Name, string Description, Scripts.Skill Skill, int Price,int strength) {
           // TODO: Complete member initialization
           this.Name = Name;
           this.Skill = Skill;
           this.Price = Price;
           this.Description = Description + " $" + Price;
           this.Strength = strength;
       }
   }

   public class Skill
   {
       public int ID;
       public string Name, Description;
           public int Value;


       public Skill(int id,string Name, string Description, int Value) {
           // TODO: Complete member initialization
           this.Name = Name;
           this.Description = Description;
           this.Value = Value;
           ID = id;
       }
   }
}
