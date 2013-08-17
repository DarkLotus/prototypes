using UnityEngine;
using System.Collections;
using Assets.Scripts;
using ProtoShared.Packets.FromClient;
using ProtoShared.Data;
/// <summary>
/// Home screens menu handler.
/// </summary>
namespace Assets.Scripts.Controllers
{

    public class PlayerController : MonoBehaviour
    {
        private NetworkManager _network;
        public Toon Toon;
        

     
        // Use this for initialization
        void Start() {
            _network = NetworkManager.Instance;//GameObject.FindGameObjectWithTag("GameController").GetComponent<NetworkManager>();
 
        }

        // Update is called once per frame
        void Update() {

        }

        void OnGUI() {
         
        }




        internal void EnterWorld(Toon enterWorld) {
            Logger.Log(enterWorld.Name + ": Toon entering world. setting location" + enterWorld.Location.ToString());
            this.Toon = enterWorld;
            transform.position = Helpers.Helper.getVector(enterWorld.Location);
        }

        internal void handleMovementSync(ProtoShared.Packets.FromServer.SyncObjectLocation syncMobile) {
            transform.position = Helpers.Helper.getVector(syncMobile.Location);
            Logger.Log(syncMobile.Serial + " Moved to : " + syncMobile.Location.ToString());
        }
    }
}