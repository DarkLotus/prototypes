using UnityEngine;
using System.Collections;


//Not for the bar, just single drop down button, rename later
public class TopMenuBar : MonoBehaviour {
	public string ButtonText = "Actions";
	private bool bShowDropDown = false;
    private bool bShowDesignWindow = false;
	// Use this for initialization
	void Start () {
		

		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void OnGUI () {
		GUI.BeginGroup(new Rect(0,0,Screen.width,500));
		if (GUI.Button (new Rect (0,0,100,50), ButtonText)) {
			bShowDropDown = true;
		}
		
		if(bShowDropDown){
		
			if (GUI.Button (new Rect (0,50,100,50), "Develop a Movie...")) {
				bShowDropDown = false;
			}
			if (GUI.Button (new Rect (0,100,100,50), "Develop a Movie")) {
				bShowDropDown = false;
			}
			
		}
		GUI.EndGroup();

        if (bShowDesignWindow)
        {

        }


	}
	
}
