using UnityEngine;
using System.Collections;

public class StaffComponent : MonoBehaviour {
    public int Wage = 0;
    public int Tech, Design, Speed;


	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<StaffController>().addStaff(this);
 
	}
	
	// Update is called once per frame
	void Update () {
       
	}
}
