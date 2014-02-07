using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public static bool gotToMenu = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {

		if(Time.time < 7.52f)
			return;

		if(gotToMenu) {
			if(StateController.CurrentState == State.MenuState) {
				if(GUI.Button(new Rect(Screen.width - 100,Screen.height - 40,80,20), "Start")) {
					StateController.CurrentState = State.DancefloorState;
				}
			}
		} else {
			gotToMenu = true;
		}
	}
}
