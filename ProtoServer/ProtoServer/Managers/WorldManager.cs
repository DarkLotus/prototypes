using ProtoServer.Data;
using ProtoShared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtoServer.Managers
{
    /// <summary>
    /// Should handle list of available scenes etc?
    /// </summary>
    public static class WorldManager
    {
        static WorldManager() {
            Scenes.Add(1, Scene.CreateDummyScene());
        }
        public static Dictionary<int, Scene> Scenes = new Dictionary<int, Scene>();

        public static void Update(long delta) {
            foreach (var s in Scenes.Values)
                s.Update(delta);
        
        }


        public static void PlayerJoinScene(Account player) {

            Scenes[player.CurrentToon.SceneSerial].ToonJoinWorld(player);//.Toons.Add(player);
        }
    }

    




}
