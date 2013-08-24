using Assets.Scripts.Controllers;
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
        List<MobileController> OtherPlayers = new List<MobileController>();

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
                case OpCodes.S_SyncObjectLocation:
                    handleSyncMobile((SyncObjectLocation)msg);
                    break;
            }
        }

        private void handleSyncMobile(SyncObjectLocation syncMobile) {
            Logger.Log("SyncMobile ID: " + syncMobile.Serial + "  " + syncMobile.Location.ToString());
            foreach (var p in OtherPlayers)
                if (p.Toon.Serial == syncMobile.Serial)
                    p.handleMovementSync(syncMobile);
        }

        private void handleOtherToon(ShowOtherToon showOtherToon) {
            if (showOtherToon.Toon == null) {
                Logger.LogError("Show Other Toon was Null"); return;
            }
            var newplayer = (GameObject)Instantiate(Resources.Load("otherPlayer"));
            var newpl = newplayer.GetComponent<MobileController>();
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
