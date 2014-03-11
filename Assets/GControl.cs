using UnityEngine;
using System.Collections;

public class GControl : SpriteControl {

	public GameObject convo;
	
	public override void init() {
		locationLookup.Add(State.DancefloorState, new Vector3(2f, 0f, 0f));
		locationLookup.Add(State.GState, new Vector3(1.825f,-6.58f,-1));
		
		convo.SetActive(false);
	}

	public override void update() {
		convo.SetActive(StateController.CurrentState == State.GState && firstLineSaid);
	}
	
	float timePassed = 0;
	bool firstLineSaid = false;
	
	void OnGUI () {
		
		if(StateController.CurrentState != State.GState || !atLocation()) {
			timePassed = 0;
			return;
		}
		
		if(!firstLineSaid && atLocation()) {
			if(GUI.Button(new Rect(Screen.width - 100,Screen.height - 40,80,20), "Keep trying man.")) {
				firstLineSaid = true;
				GetComponentInChildren<Dance>().dancing = false;
			}
			return;
		}
		
		timePassed += Time.deltaTime;
		
		if(timePassed > 3) {
			if(GUI.Button(new Rect(Screen.width - 100,Screen.height - 40,80,20), "...")) {
				StateController.CurrentState = State.DancefloorState;
				GetComponentInChildren<Dance>().dancing = true;
				firstLineSaid = false;
			}
		}
	}
}
