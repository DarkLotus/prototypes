using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using System;

//[SerializeAll]
public class GameReleaseManager : MonoBehaviourEx
{
   
    
    public List<GameItem> ReleasedGames = new List<GameItem>();
    [SerializeField]
    private GameItem _inDevGame;
    public float complete = 0;


    GameTimeController _gtc;
    StaffController _sc;
    // Use this for initialization
    void Start() {
        _gtc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameTimeController>();
        _sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<StaffController>();
        _gtc.DayElasped += _gtc_DayElasped;

        if (Application.platform == RuntimePlatform.Android) {
            Debug.Log("Trying to Call finish");
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            
            activity.Call("finish");
        }
    }

    void _gtc_DayElasped() {
        Console.WriteLine("Day Elasped");
        if (_inDevGame != null) {
            complete += _sc.GetDaysWorkPercent(_inDevGame);
        }
      
    }

    private void ReleaseGame() {
        ReleasedGames.Add(_inDevGame);
        _inDevGame = null;
        
    }

    // Update is called once per frame
    void Update() {
        if (bShowReviews || bShowAddEngFeature || bShowAddGameFeature)
            _gtc.GUIOpen = true;
        else
            _gtc.GUIOpen = false;
        if (complete > 25 && !bShownEngFeatures)
            bShowAddEngFeature = true;
        if (complete > 50 && !bShownGameFeatures)
            bShowAddGameFeature = true;
        if (complete >= 100 && !bShownReview) {
            //TODO let them remove bugs etc longer QA etc.
            bShowReviews = true;

        }
        if (complete >= 100 && !bShowReviews) {
            ReleaseGame();
        }


    }

    public bool CreateNewGame(GameItem game) {
        if (_inDevGame != null)
            return false;
            _inDevGame = game;
        _sc.StartWork();
        GUIHelpers.resetGameFeatures();
        return true;
    }
   
    void OnGUI() {
        if(_inDevGame != null)
            GUI.Label(new Rect(Screen.width /2, 100, 100, 50), "Complete %" + complete);
        if (bShowAddEngFeature) {
            bShowAddEngFeature = GUIHelpers.DrawAddEngFeaturesMenu(_inDevGame);
            bShownEngFeatures = !bShowAddEngFeature;
        }
        if (bShowAddGameFeature) {
            bShowAddGameFeature = GUIHelpers.DrawAddGameFeaturesMenu(_inDevGame);
            bShownGameFeatures = !bShowAddGameFeature;
        }
        if (bShowReviews) {
            bShowReviews = GUIHelpers.DrawReviewScreen(_inDevGame);
            bShownReview = !bShowReviews;
        }


        
    }

    public bool bShowAddEngFeature { get; set; }

    public bool bShowAddGameFeature { get; set; }

    public bool bShowReviews { get; set; }

    public bool bShownReview { get; set; }

    public bool bShownEngFeatures { get; set; }

    public bool bShownGameFeatures { get; set; }
}
