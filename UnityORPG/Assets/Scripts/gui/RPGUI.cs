using UnityEngine;
using System.Collections;
using Assets.Scripts;
using ProtoShared.Packets.FromClient;
/// <summary>
/// Home screens menu handler.
/// </summary>
namespace Assets.Scripts.gui
{

    public class RPGUI : MonoBehaviour
    {
        public Camera CurrentCamera;
        private bool bShowCharSelect;
        private string[] charList;
        NetworkManager _network;
        private bool bConnected;
        // Use this for initialization
        void Start() {
            _network = GameObject.FindGameObjectWithTag("GameController").GetComponent<NetworkManager>();
 
        }

        // Update is called once per frame
        void Update() {

        }

        void OnGUI() {
            
                GUILayout.BeginArea(new Rect(Screen.width / 2, Screen.height / 2, 300, 600));
                if (!bConnected) {
                    if (GUILayout.Button("Connect")) {

                        //_network.Connect();
                    }
                }
            if (bShowCharSelect) {
               
                for (int i = 0; i < charList.Length; i++) {

                    if (GUILayout.Button(charList[i])) {
                        _network.Send(new SelectCharacter(i));
                    }
                }
                if (GUILayout.Button("New Character")) { bShowCharSelect = false; _network.Send(new CreateCharacter() { Name = "test", isMale = false }); }
               
            }
            GUILayout.EndArea();
        }

 

        internal void DisplayCharacterSelect(ProtoShared.Packets.FromServer.LoginResponse loginResponse) {
            bShowCharSelect = true;
            this.charList = loginResponse.Characters;
            if (charList == null)
                charList = new string[0];
        }



        internal void EnteredWorld() {
            Logger.Log("Entered world called in UI, disabling UI Camera");
            CurrentCamera.enabled = false;
            bShowCharSelect = false;
            bConnected = true;
        }
    }
}