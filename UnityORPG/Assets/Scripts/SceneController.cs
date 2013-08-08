using ProtoShared;
using ProtoShared.Data;
using ProtoShared.Packets.FromServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts
{
    public class SceneController : MonoBehaviour
    {
        PlayerController player;
        List<PlayerController> OtherPlayers = new List<PlayerController>();

        void Start() {
            if (NetworkManager.Instance == null)
                Logger.LogError("Networkmanager was null");// Add connectedcheck etc
            NetworkManager.Instance.OnPacketarrival += Instance_OnPacketarrival;
            NetworkManager.Instance.GetQueuedMessages();
            var newplayer = (GameObject)Instantiate(Resources.Load("Joanrpg"));
            player = newplayer.GetComponent<PlayerController>();
            player.EnterWorld(NetworkManager.Instance.PlayerToon);
        }

        void Instance_OnPacketarrival(ProtoShared.Packets.BaseMessage msg) {
            switch (msg.PacketType) {
                case OpCodes.S_ShowOtherToon:
                    handleOtherToon((ShowOtherToon)msg);
                    break;
                case OpCodes.S_SyncMobile:
                    handleSyncMobile((SyncMobile)msg);
                    break;
            }
        }

        private void handleSyncMobile(SyncMobile syncMobile) {
            foreach (var p in OtherPlayers)
                if (p.Toon.Serial == syncMobile.Serial)
                    p.handleMovementSync(syncMobile);
        }

        private void handleOtherToon(ShowOtherToon showOtherToon) {
            
            var newplayer = (GameObject)Instantiate(Resources.Load("otherPlayer"));
            var newpl = newplayer.GetComponent<PlayerController>();
            newpl.EnterWorld(showOtherToon.Toon);
            OtherPlayers.Add(newpl);

        }

        void Update() {
            foreach (var p in OtherPlayers) { }
        }



        /*        var player = (GameObject)Instantiate(Resources.Load("Joanrpg"));
        Logger.Log("Player fab created");
        player.GetComponent<PlayerController>().EnterWorld(enterWorld);*/
    }
}
