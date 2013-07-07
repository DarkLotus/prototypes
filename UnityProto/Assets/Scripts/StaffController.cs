using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
public class StaffController : MonoBehaviour {

    public List<StaffComponent> Staff = new List<StaffComponent>();
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
     
        
	}
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - 400, 0, 100, 50), Staff.Count + "Staff Members");
        
    }

    internal void addStaff(StaffComponent staffComponent) {
        Staff.Add(staffComponent);
    }

    public void StartWork() {
        foreach (StaffComponent sc in Staff) {
            sc.State = StaffComponent.HumanState.Typing;
        }
    }

    public void StopWork() {
        foreach (StaffComponent sc in Staff) {
            sc.State = StaffComponent.HumanState.Idle;
        }
    }
    /// <summary>
    /// Gets the percent progress per day for the current game
    /// Formula of employees(stats) + game size/ features etc
    /// </summary>0-1 range returned for now.
    /// <param name="game"></param>
    /// <returns></returns>
    public float GetDaysWorkPercent(GameItem game) {
        float val = 0;
        foreach (StaffComponent s in Staff) {
            val += (s.Programming + s.Art);
        }
        //val /= game.Scope;
        return val;
    }
}
