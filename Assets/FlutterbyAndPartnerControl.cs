using UnityEngine;
using System.Collections;

public class FlutterbyAndPartnerControl : SpriteControl {
	public GameObject convo;
	
	public override void init() {
		locationLookup.Add(State.FlutterbyPartnerState, new Vector3(4.028455f,-10.40227f,-1));
		
		convo.SetActive(false);
	}
	
	public override void update() {
		convo.SetActive(StateController.CurrentState == State.FlutterbyPartnerState && firstLineSaid);
	}
	
	float timePassed = 0;
	bool firstLineSaid = false;
	
	void OnGUI () {
		
		if(StateController.CurrentState != State.FlutterbyPartnerState || !atLocation()) {
			timePassed = 0;
			return;
		}
		
		if(!firstLineSaid && atLocation()) {
			if(GUI.Button(new Rect(Screen.width - 100,Screen.height - 40,80,20), "Hey")) {
				firstLineSaid = true;
				GetComponentInChildren<Dance>().dancing = false;
			}
			return;
		}
		
		timePassed += Time.deltaTime;
		
		if(timePassed > 3) {
			if(GUI.Button(new Rect(Screen.width - 100,Screen.height - 40,80,20), "No, I just...")) {
				StateController.CurrentState = State.DancefloorState;
				GetComponentInChildren<Dance>().dancing = true;
				firstLineSaid = false;
			}
		}
	}
}
