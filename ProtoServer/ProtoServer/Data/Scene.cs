using ProtoServer.Managers;
using ProtoShared;
using ProtoShared.Data;
using ProtoShared.Packets;
using ProtoShared.Packets.FromClient;
using ProtoShared.Packets.FromServer;
using ProtoShared.Packets.Shared;
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
        public Scene() {
            ClientManager.Instance.OnPacketarrival += ClientManager_OnPacketarrival;
        }


        public void Update(long delta) { }

        internal static Scene CreateDummyScene() {
            Scene s = new Scene();
            s.Serial = 1;
            s.Name = "TestScene";
            return s;

        }


        /// <summary>
        /// Called when a player logs on and his toon joins a scene.
        /// </summary>
        /// <param name="player"></param>
        internal void ToonJoinWorld(Account player) {
            Logger.Log(player.CurrentToon.Name + " Joined Scene : " + Serial + " Sending reveal to " + Toons.Count + " clients");
            ShowOtherToon show = new ShowOtherToon(player.CurrentToon);
            foreach (var t in Toons) {
                show.Send(t); // Reveal new player to old players.
                new ShowOtherToon(t.CurrentToon).Send(player);//.Send(player.Client.GetStream());// Reveal current players to new player
                
            }
            Toons.Add(player);
        }


       
        /// <summary>
        /// Called when a player logs off or is dissconnected etc.
        /// </summary>
        /// <param name="player"></param>
        internal void ToonLeaveWorld(Account player) {
            if (!Toons.Contains(player))
                return;
            Logger.Log(player.CurrentToon.Name + " Left Scene : " + Serial + " Sending Remove to " + Toons.Count + " clients");
            //TODO send player gone packet.

            Toons.Remove(player);
            foreach (var t in Toons) {
                new DeleteObject(player.CurrentToon.Serial).Send(t.Client.GetStream());
            }
        }


        /// <summary>
        /// Packet handler for Scene related Messages
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="msg"></param>
        void ClientManager_OnPacketarrival(Account owner, ProtoShared.Packets.BaseMessage msg) {
            switch (msg.PacketType) {
                case OpCodes.C_MoveRequest:
                    handleMoveRequest(owner.CurrentToon, (MoveRequest)msg);
                    break;
                case OpCodes.S_ChatMessage:
                    handleChatMessage(owner, (ChatMessage)msg);
                    break;
            }
        }



        private void handleChatMessage(Account a, ChatMessage chatMessage) {
            Logger.Log(chatMessage.Sender + ": " + chatMessage.Message);
            foreach (var p in Toons)
                if (a.Serial != p.Serial)
                    chatMessage.Send(p.Client.GetStream());
        }

        private void handleMoveRequest(Toon p, MoveRequest syncClient) {
            Logger.Log(p.Name + " Moved to " + syncClient.Location);
            p.Location.Set(syncClient.Location);
            SendMovementUpdate(syncClient, p.Serial);

        }
        /// <summary>
        /// Handles sending out movement update messages to appropriate toons
        /// </summary>
        /// <param name="syncClient"></param>
        /// <param name="serial"></param>
        private void SendMovementUpdate(MoveRequest syncClient, int serial) {
            SyncObjectLocation m = new SyncObjectLocation();
            m.Serial = serial;
            m.Location = new Vector3D(syncClient.Location);
            foreach (Account p in Toons)
                if (p.CurrentToon.Serial != serial)
                    m.Send(p.Client.GetStream());
            Logger.Log("Sent SyncMobile to: " + Toons.Count + " players");
        }







    }
}
