using UnityEngine;
using System.Collections;
using Assets.Scripts;


//Not for the bar, just single drop down button, rename later
public class TopMenuBar : MonoBehaviour {
	public string ButtonText = "Actions";

    public GameItem _currentGame;
    public GameTimeController _gtc { get; set; }


    private bool bShowResearchWindow = false;
    private bool bShowSubgenreWindow = false;
    private bool bShowGenreWindow = false;
    private bool bShowHireStaffWindow = false;
    private bool bShowDropDown = false;
    private bool bShowDesignWindow = false;

	// Use this for initialization
	void Start () {
        _gtc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameTimeController>();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (bShowDesignWindow || bShowHireStaffWindow || bShowResearchWindow)
            _gtc.GUIOpen = true;
        else
            _gtc.GUIOpen = false;
	}


	void OnGUI () {
        DrawTopMenuBar();
		

        if (bShowDesignWindow && !bShowGenreWindow && !bShowSubgenreWindow)
            DrawDesignGameWindow();
        if (bShowGenreWindow)
            bShowGenreWindow = GUIHelpers.DrawGenreSelect(_currentGame);
        if (bShowSubgenreWindow)
            bShowSubgenreWindow = GUIHelpers.DrawSubGenreSelect(_currentGame); ;
        if (bShowHireStaffWindow)
            bShowHireStaffWindow = GUIHelpers.DrawHireStaff();
        if (bShowResearchWindow)
            bShowResearchWindow = GUIHelpers.DrawResearchWindow();

	}

   
    private void DrawTopMenuBar() {
        
        //TODO use guilayout
        GUILayout.BeginArea(new Rect(0, 0, Screen.width / 3, Screen.height));
        //GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        if (GUILayout.Button(ButtonText)) {
            if (!bShowDropDown) {
                bShowDropDown = true;
            }
            else
                bShowDropDown = false;
            
        }

        if (bShowDropDown) {
            if (GUILayout.Button("Develop a Game...")) {
                bShowDesignWindow = true;
                bShowDropDown = false;
                _currentGame = new GameItem();
            }
            if (GUILayout.Button("Research Features...")) {
                bShowResearchWindow = true;
                bShowDropDown = false;
            }
            if (GUILayout.Button("Hire Staff")) {
                bShowHireStaffWindow = true;
                bShowDropDown = false;
            }
            if (GUILayout.Button("Save Game")) {
                bShowDropDown = false;
                LevelSerializer.SaveGame(_gtc.Date);
            }

            if (GUILayout.Button("Load Last Game")) {
                //Application.LoadLevel(0);
                var g = LevelSerializer.SavedGames[LevelSerializer.PlayerName].ToArray()[0];
                LevelSerializer.LoadNow(g.Data, false, true);
                bShowDropDown = false;
                
               // g.Load();
            }
            if (GUILayout.Button("Quit Game")) {
                bShowDropDown = false;
                Application.Quit();
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();

    }

   


    private int _gameCost = 0;

    public static string[] ratingStrings = new string[] { "All", "PG", "M", "MA", "R" };

    
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
            _currentGame.Scope = GUILayout.SelectionGrid(_currentGame.Scope, GameValues.GameScope, 4);
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


    /// <summary>
    /// Called when player hits Okay on create game.
    /// </summary>
    private void createGame() {
        
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameReleaseManager>().CreateNewGame(_currentGame);
        _currentGame = null;
        
    }

   
   


  

    

   
}
