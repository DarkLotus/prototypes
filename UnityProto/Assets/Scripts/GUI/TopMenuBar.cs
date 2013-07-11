using UnityEngine;
using System.Collections;
using Assets.Scripts;


//Not for the bar, just single drop down button, rename later
public class TopMenuBar : MonoBehaviour {
	public string ButtonText = "Actions";
	private bool bShowDropDown = false;
    private bool bShowDesignWindow = false;
    public GameItem _currentGame;

	// Use this for initialization
	void Start () {

        _gtc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameTimeController>();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnGUI () {
        DrawTopMenuBar();
		

        if (bShowDesignWindow && !bShowGenreWindow && !bShowSubgenreWindow)
            DrawDesignGameWindow();
        if (bShowGenreWindow)
            DrawGenreSelect();
        if (bShowSubgenreWindow)
            DrawSubgenreSelect();

	}

    private void DrawTopMenuBar() {
        //TODO use guilayout
        GUI.BeginGroup(new Rect(0, 0, Screen.width, 500));
        if (GUI.Button(new Rect(0, 0, 100, 50), ButtonText)) {
            bShowDropDown = true;
        }

        if (bShowDropDown) {

            if (GUI.Button(new Rect(0, 50, 100, 50), "Develop a Game...")) {
                bShowDesignWindow = true;
                bShowDropDown = false;
                _currentGame = new GameItem();
            }
            if (GUI.Button(new Rect(0, 100, 100, 50), "Research...")) {
                bShowDropDown = false;
            }
            if (GUI.Button(new Rect(0, 150, 100, 50), "Save Game")) {
                bShowDropDown = false;
                LevelSerializer.SaveGame(_gtc.Date);
            }

            if (GUI.Button(new Rect(0, 200, 100, 50), "Load Game")) {
                bShowDropDown = false;
                var g = LevelSerializer.SavedGames[LevelSerializer.PlayerName].ToArray()[0];
                g.Load();
            }
            if (GUI.Button(new Rect(0, 250, 100, 50), "Quit Game")) {
                bShowDropDown = false;
                Application.Quit();


            }


        }
        GUI.EndGroup();
    }

   


    private int _gameCost = 0;

    public static string[] ratingStrings = new string[] { "All", "PG", "M", "MA", "R" };

    private bool bShowGenreWindow = false;
    private void DrawDesignGameWindow() {
        if (bShowDesignWindow) {
            GUI.BeginGroup(new Rect(200, 150, Screen.width - 400, Screen.height - 200));

            GUI.Box(new Rect(0, 0, Screen.width - 400, Screen.height - 200), "");
            if (GUI.Button(new Rect(Screen.width - 425, 0, 25, 25), "X")) {
                bShowDesignWindow = false;
            }
            GUI.EndGroup();
            GUILayout.BeginArea(new Rect(200, 150, Screen.width - 400, Screen.height - 200));
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            GUILayout.Label("Total Cost:");
            GUILayout.Label("Name:");
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            GUILayout.Label("$" + _gameCost);
            _currentGame.Name = GUILayout.TextField("Game #");
            
            
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Atmosphere: Light/Dark");
            _currentGame.FeelFactor = GUILayout.HorizontalSlider(_currentGame.FeelFactor, 0.0f, 10f);
            GUILayout.EndHorizontal();
            GUILayout.BeginVertical();
            _currentGame.Rating = GUILayout.SelectionGrid(_currentGame.Rating, ratingStrings, 5);
            GUILayout.BeginHorizontal();
            string genrebutton = "Genre";
            if (_currentGame.Genre != 0xFFF)
                genrebutton = "Genre:" + GameValues.genreStrings[_currentGame.Genre];
            if (GUILayout.Button(genrebutton)) {
                bShowGenreWindow = true;
            }

            string subgenrebutton = "Subgenre:";
            if (_currentGame.SubGenre != 0xFFF)
                subgenrebutton = "Subgenre:" + GameValues.genreStrings[_currentGame.SubGenre];
            if (GUILayout.Button(subgenrebutton)) {
                bShowSubgenreWindow = true;
            }

           
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Okay")) {
                createGame();
                bShowDesignWindow = false;
            }
            GUILayout.EndVertical();
            GUILayout.EndArea();


        }


    }

    private void createGame() {
        
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameReleaseManager>().CreateNewGame(_currentGame);
        _currentGame = null;
        
    }

    private bool bShowSubgenreWindow = false;

   

    private void DrawGenreSelect() {
        if (_currentGame.Genre != 0xFFF)
            bShowGenreWindow = false;
        
        GUI.Box(new Rect(200, 150, Screen.width - 400, Screen.height - 200), "");
        GUILayout.BeginArea(new Rect(200, 150, Screen.width - 400, Screen.height - 200));
        GUILayout.BeginHorizontal();
        _currentGame.Genre = GUILayout.SelectionGrid(_currentGame.Genre, GameValues.genreStrings, 5);
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    private void DrawSubgenreSelect() {
        if (_currentGame.SubGenre != 0xFFF)
            bShowSubgenreWindow = false;

        GUI.Box(new Rect(200, 150, Screen.width - 400, Screen.height - 200), "");
        GUILayout.BeginArea(new Rect(200, 150, Screen.width - 400, Screen.height - 200));
        GUILayout.BeginHorizontal();
        _currentGame.SubGenre = GUILayout.SelectionGrid(_currentGame.SubGenre, GameValues.genreStrings, 5);
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }


    public GameTimeController _gtc { get; set; }
}
