using UnityEngine;
using System.Collections;
using Assets.Scripts;
/// <summary>
/// Home screens menu handler.
/// </summary>
namespace Assets.Scripts.gui
{

    public class RPGUI : MonoBehaviour
    {

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        void OnGUI() {
            GUILayout.BeginArea(new Rect(Screen.width / 2, Screen.height / 2, 300, 600));
            if (GUILayout.Button("Connect")) {

                GameObject.FindGameObjectWithTag("GameController").GetComponent<NetworkManager>().Connect();
            }


            GUILayout.EndArea();


        }
    }
}