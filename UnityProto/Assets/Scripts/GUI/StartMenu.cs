using UnityEngine;
using System.Collections;
/// <summary>
/// Home screens menu handler.
/// </summary>
public class StartMenu : MonoBehaviour {
    private bool bLoadMenu;

    public static class StartValues
    {
        public static string SaveGameToLoad;
        public static bool bLoadSaveGame;
    }
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI() {
        if (bLoadMenu) {
            GUILayout.BeginArea(new Rect(Screen.width / 2, Screen.height / 2, 300, 600));
            foreach (var g in LevelSerializer.SavedGames[LevelSerializer.PlayerName]) {
                if (GUILayout.Button(g.Caption)) {
                    g.Load();
                }

            }
            GUILayout.EndArea();
        
        }
        // Make a group on the center of the screen
        GUI.BeginGroup(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 130));
        // All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

        // We'll make a box so you can see where the group is on-screen.
        GUI.Box(new Rect(0, 0, 100, 130), "Game Dev Sim");
        if (GUI.Button(new Rect(10, 40, 80, 30), "New Game")) {
            StartValues.bLoadSaveGame = false;
            Application.LoadLevel("office");
        }
        if(GUI.Button(new Rect(10, 70, 80, 30), "Load Game")){
            StartValues.bLoadSaveGame = true;
            StartValues.SaveGameToLoad = "test";
            bLoadMenu = true;
            //Application.LoadLevel("office");

        }
        if (GUI.Button(new Rect(10, 100, 80, 30), "Quit Game")) {
            Application.Quit();
        }
        // End the group we started above. This is very important to remember!
        GUI.EndGroup();
       
    }
}
