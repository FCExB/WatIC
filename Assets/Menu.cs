using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
	
		if(StateController.CurrentState == State.MenuState && StateController.GotToMenu 
		   && GUI.Button(new Rect(Screen.width*0.2f,Screen.height*0.9f,
		                          Screen.width*0.2f,Screen.height*0.05f), "Start")) {
			StateController.CurrentState = State.DancefloorState;
		}
	}
}
