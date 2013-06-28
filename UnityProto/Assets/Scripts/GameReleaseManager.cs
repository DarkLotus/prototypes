using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using System;
public class GameReleaseManager : MonoBehaviour
{
    public List<GameItem> ReleasedGames = new List<GameItem>();
    public GameItem InDevGame;
    public int complete = 0;


    GameTimeController _gtc;
    // Use this for initialization
    void Start() {
        _gtc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameTimeController>();
        _gtc.DayElasped += _gtc_DayElasped;
    }

    void _gtc_DayElasped() {
        Console.WriteLine("Day Elasped");
        if (InDevGame != null) {
            complete += 1;
        }
        if (complete == 100) {
            ReleasedGames.Add(InDevGame);
            InDevGame = null;
        }
    }

    // Update is called once per frame
    void Update() {
       

    }

   
    void OnGUI() {
        if(InDevGame != null)
        GUI.Label(new Rect(Screen.width /2, 100, 100, 50), "Complete %" + complete);
        
    }
}
