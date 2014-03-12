using UnityEngine;
using System.Collections;

public class FlutterbyControl : SpriteControl {

	public GameObject convo;

	public override void init() {
		locationLookup.Add(State.FlutterbyState, new Vector3(-6.61f,-6.25f,-1));
		locationLookup.Add (State.FlutterbyLeavesState, new Vector3(-15f,-6.35f,-1));

		convo.SetActive(false);
	}

	public override void update() {
		convo.SetActive((StateController.CurrentState == State.FlutterbyState || 
		                 	StateController.CurrentState == State.FlutterbyLeavesState) 
		                && firstLineSaid);
	}

	float timePassed = 0;

	bool firstLineSaid = false;

	void OnGUI () {

		if(!(StateController.CurrentState == State.FlutterbyState || 
		   StateController.CurrentState == State.FlutterbyLeavesState)) {
			timePassed = 0;
			GetComponentInChildren<Dance>().dancing = true;
			return;
		}

		if(!firstLineSaid) {
			if(atLocation()) {
				if(GUI.Button(new Rect(Screen.width*0.55f,Screen.height*0.19f,
				                       Screen.width*0.2f,Screen.height*0.08f), "I Like you")) {
					firstLineSaid = true;
				}
			}
			return;
		}

		if(StateController.CurrentState == State.FlutterbyLeavesState) {
			timePassed += Time.deltaTime;
			
			if(timePassed > 3) {
				if(GUI.Button(new Rect(Screen.width*0.7f,Screen.height*0.8f,
				                       Screen.width*0.2f,Screen.height*0.08f), "...")) {
					GetComponentInChildren<Dance>().dancing = false;
					StateController.CurrentState = State.DancefloorState;
					firstLineSaid = false;
				}
			}
			return;
		} 

		timePassed += Time.deltaTime;

		if(timePassed > 7) {
			GetComponentInChildren<Dance>().dancing = false;
			StateController.CurrentState = State.FlutterbyLeavesState;
			timePassed = 0;
		}
	}
}
