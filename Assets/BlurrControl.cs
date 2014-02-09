using UnityEngine;
using System.Collections;

public class BlurrControl : SpriteControl {

	public GameObject convo;
	
	public override void init() {
		locationLookup.Add(State.BlurrState, new Vector3(5.125f,2.6f,-1));
		
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
			if(GUI.Button(new Rect(Screen.width - 100,Screen.height - 40,80,20), "You look sad")) {
				firstLineSaid = true;
			}
		}

		timePassed += Time.deltaTime;
		
		if(timePassed > 8) {
			if(GUI.Button(new Rect(Screen.width - 100,Screen.height - 40,80,20), "*weird")) {
				StateController.CurrentState = State.DancefloorState;
			}
		}
	}
}
