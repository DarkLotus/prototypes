using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
public class StaffController : MonoBehaviour {



    public StaffComponent[] Staff;
    
        public int StaffCount = 0;

	void Start () {
        Staff = new StaffComponent[5];
	}
	
	// Update is called once per frame
	void Update () {
     
        
	}
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - 400, 0, 100, 50), StaffCount + "Staff Members");
        
    }

    internal void addStaff(StaffComponent staffComponent) {
        Logger.Log("addstaff Called");
        Staff[StaffCount] = staffComponent;
        StaffCount++;
    }

    public void StartWork() {
        foreach (StaffComponent sc in Staff) {
            if(sc != null)
            sc.State = StaffComponent.HumanState.Typing;
        }
    }

    public void StopWork() {
        foreach (StaffComponent sc in Staff) {
            if (sc != null)
            sc.State = StaffComponent.HumanState.Idle;
        }
    }
    /// <summary>
    /// Gets the percent progress per day for the current game
    /// Formula of employees(stats) + game size/ features etc
    /// </summary>0-1 range returned for now.
    /// <param name="game"></param>
    /// <returns></returns>
    internal float GetDaysWorkPercent(GameItem game) {
        int val = 0;
        foreach (StaffComponent s in Staff) {
            if (s != null)
            val += (s.Programming + s.Art) / 100;
        }
        //val /= game.Scope;
        return val / (game.Scope+1); // 0=small 1=medium
    }

    internal int GetWages() {
        int val = 0;
        foreach (StaffComponent s in Staff) {
            if(s != null)
            val += (s.Wage);
        }
        
        return val;
    }
}
