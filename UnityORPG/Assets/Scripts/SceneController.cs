using ProtoShared.Data;
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
            throw new NotImplementedException();
        }

        void Update() { }



        /*        var player = (GameObject)Instantiate(Resources.Load("Joanrpg"));
        Logger.Log("Player fab created");
        player.GetComponent<PlayerController>().EnterWorld(enterWorld);*/
    }
}
