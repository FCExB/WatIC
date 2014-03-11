using UnityEngine;
using System.Collections;

public class BlurrControl : SpriteControl {

	public GameObject convo;
	
	public override void init() {
		locationLookup.Add(State.BlurrState, new Vector3(5.125f,2.6f,-1));
		locationLookup.Add(State.SaveState, new Vector3(-1.03f,-8.85f, -1f));
		
		convo.SetActive(false);
	}
	
	public override void update() {
		convo.SetActive(StateController.CurrentState == State.BlurrState && firstLineSaid);
	}

	float timePassed = 0;
	bool firstLineSaid = false;
	
	void OnGUI () {
		
		if(StateController.CurrentState != State.BlurrState || !atLocation()) {
			timePassed = 0;
			return;
		}

		if(!firstLineSaid && atLocation()) {
			if(GUI.Button(new Rect(Screen.width*0.27f,Screen.height*0.2f,
			                       Screen.width*0.25f,Screen.height*0.1f), "You look sad")) {
				firstLineSaid = true;
			}
			return;
		}

		timePassed += Time.deltaTime;
		
		if(timePassed > 8) {
			if(GUI.Button(new Rect(Screen.width*0.7f,Screen.height*0.78f,
			                       Screen.width*0.1f,Screen.height*0.05f), "*weird")) {
				StateController.CurrentState = State.DancefloorState;
				firstLineSaid = false;
			}
		}
	}
}
