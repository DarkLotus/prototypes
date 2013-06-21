using UnityEngine;
using System.Collections;

public class GameTimeController : MonoBehaviour {
    public int Money = 50000;
    public float _elapsedTime = 0;
    public int Day, Month, Year = 1970;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > 2)
        {
            Day++; _elapsedTime = 0;
        }
        if (Day > 30)
        {
            Money -= getMonthsExpenses();

            Month++; Day = 1;
        }
        if (Month > 12)
        {
            Year++;
            Month = 1;
        }
        
	}

    private int getMonthsExpenses() {
        throw new System.NotImplementedException();
    }
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - 100, 0, 100, 50), "" + Day + "/" + Month + "/" + Year);
        GUI.Label(new Rect(Screen.width - 200, 0, 100, 50), "$" + Money);
    }
}
