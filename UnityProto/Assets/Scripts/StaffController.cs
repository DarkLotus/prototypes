using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
}
