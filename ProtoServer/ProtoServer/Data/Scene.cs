using ProtoShared.Data;
using ProtoShared.Packets.FromServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProtoServer.Data
{
    public class Scene
    {
        public string Name;
        public int Serial;
        public List<Item> Items = new List<Item>();
        public List<Mobile> Mobiles = new List<Mobile>();
        public List<Account> Toons = new List<Account>();

        public void Update(long delta) { }

        internal static Scene CreateDummyScene() {
            Scene s = new Scene();
            s.Serial = 1;
            s.Name = "TestScene";
            return s;

        }

        internal void ToonJoinWorld(Account player) {
            Logger.Log(player.CurrentToon.Name + " Joined Scene : " + Serial + " Sending reveal to " + Toons.Count + " clients");
            ShowOtherToon show = new ShowOtherToon(player.CurrentToon);
            foreach (var t in Toons)
                show.Send(t.Client.GetStream());
            Toons.Add(player);
        }

        internal void ToonLeaveWorld(Account player) {
            Logger.Log(player.CurrentToon.Name + " Left Scene : " + Serial + " Sending Remove to " + Toons.Count + " clients");
            //TODO send player gone packet.

            Toons.Remove(player);
        }
    }
}
