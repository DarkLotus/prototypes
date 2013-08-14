using ProtoServer.Data;
using ProtoShared;
using ProtoShared.Data;
using ProtoShared.Packets.FromClient;
using ProtoShared.Packets.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace ProtoServer.Managers
{
    /// <summary>
    /// Handles all avalable scenes, In future this could be used to split Scene servers among nodes/threads
    /// </summary>
    public static class WorldManager
    {
        static WorldManager() {
            //TODO scenes should be loaded from DB/XML exported from unity, holding terrain/world objects etc
            Scenes.Add(1, Scene.CreateDummyScene());
            
        }

       
        public static Dictionary<int, Scene> Scenes = new Dictionary<int, Scene>();

        /// <summary>
        /// Tick
        /// </summary>
        /// <param name="delta"></param>
        public static void Update(long delta) {
            Scenes.Values.EachParallel(s => { s.Update(delta); });
           /* foreach (var s in Scenes.Values)
                s.Update(delta);*/
        
        }


        internal static void PlayerJoinScene(Account player) {

            Scenes[player.CurrentToon.SceneSerial].ToonJoinWorld(player);//.Toons.Add(player);
        }

        internal static void PlayerLeaveScene(Account player) {
            if(player.CurrentToon.SceneSerial != 0)
            Scenes[player.CurrentToon.SceneSerial].ToonLeaveWorld(player);
        }


       
    }

    




}
