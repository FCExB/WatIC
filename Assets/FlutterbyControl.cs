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
		convo.SetActive(StateController.CurrentState == State.FlutterbyState && atLocation());
	}

	float timePassed = 0;

	void OnGUI () {

		if(StateController.CurrentState == State.FlutterbyLeavesState) {
			timePassed += Time.deltaTime;
			
			if(timePassed > 3) {
				if(GUI.Button(new Rect(Screen.width - 100,Screen.height - 40,80,20), "Back")) {
					GetComponentInChildren<Dance>().dancing = false;
					StateController.CurrentState = State.DancefloorState;
				}
			}
			return;
		} 

		if(StateController.CurrentState != State.FlutterbyState || !atLocation()) {
			timePassed = 0;
			GetComponentInChildren<Dance>().dancing = true;
			return;
		}
		timePassed += Time.deltaTime;

		if(timePassed > 5) {
			if(GUI.Button(new Rect(Screen.width - 100,Screen.height - 40,80,20), "*leaves*")) {
				GetComponentInChildren<Dance>().dancing = false;
				StateController.CurrentState = State.FlutterbyLeavesState;
				timePassed = 0;
			}
		}
	}
}
