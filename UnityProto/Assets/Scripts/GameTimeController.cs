using UnityEngine;
using System.Collections;
using System;

public class GameTimeController : MonoBehaviourEx
{
    public delegate void MonthElaspedHandler();
    public event MonthElaspedHandler MonthElasped;
    public delegate void DayElaspedHandler();
    public event DayElaspedHandler DayElasped;

    public float _elapsedTime = 0;
    public int Day, Month, Year = 1970;

    public string Date { get { return "" + Day + "-" + Month + "-" + Year; } }
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if (GUIOpen)
            return;
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > 0.5f)
        {
            Day++; _elapsedTime = 0;
            if (DayElasped != null)
                DayElasped();
        }
        if (Day > 30)
        {
            Month++; Day = 1;
            if (MonthElasped != null)
                MonthElasped();
        }
        if (Month > 12)
        {
            Year++;
            Month = 1;
        }
        
	}


    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - 100, 0, 100, 50), "" + Day + "/" + Month + "/" + Year);
    }

    public bool GUIOpen { get; set; }
}
