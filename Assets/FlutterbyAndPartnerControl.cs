using UnityEngine;
using System.Collections;

public class FlutterbyAndPartnerControl : SpriteControl {
	public GameObject convo;
	public GameObject interConvo;
	
	public override void init() {
		locationLookup.Add(State.FlutterbyPartnerState, new Vector3(4.028455f,-10.40227f,-1));
		locationLookup.Add(State.SaveState, new Vector3(2.48f,-10.40227f,-1));
		
		convo.SetActive(false);
		interConvo.SetActive(false);
	}
	
	public override void update() {
		convo.SetActive((StateController.CurrentState == State.FlutterbyPartnerState && firstLineSaid)
		                 || StateController.CurrentState == State.SaveState);
		interConvo.SetActive(interrupt && timePassed > 2);
	}
	
	float timePassed = 0;
	bool firstLineSaid = false;
	bool interrupt = false;
	
	void OnGUI () {
		
		if(!(StateController.CurrentState == State.FlutterbyPartnerState
		     		|| StateController.CurrentState == State.SaveState) || !atLocation()) {
			timePassed = 0;
			return;
		}
		
		if(!firstLineSaid && !interrupt && atLocation()) {
			if(GUI.Button(new Rect(Screen.width*0.1f,Screen.height*0.2f,
			                       Screen.width*0.1f,Screen.height*0.09f), "Hey")) {
				firstLineSaid = true;
				GetComponentInChildren<Dance>().dancing = false;
			}
			return;
		}
		
		timePassed += Time.deltaTime;
		
		if(firstLineSaid && timePassed > 3) {
			if(GUI.Button(new Rect(Screen.width*0.08f,Screen.height*0.7f,
			                                  Screen.width*0.23f,Screen.height*0.09f), "No, I just...")) {
				StateController.CurrentState = State.SaveState;
				timePassed = 0;
				firstLineSaid = false;
				interrupt = true;
			}
		}

		if(interrupt && timePassed > 4) {
			if(GUI.Button(new Rect(Screen.width*0.68f,Screen.height*0.81f,
			                       Screen.width*0.1f,Screen.height*0.09f), "Ok")) {
				StateController.CurrentState = State.DancefloorState;
				GetComponentInChildren<Dance>().dancing = true;
				timePassed = 0;
				interrupt = false;
				interConvo.SetActive(false);
				convo.SetActive(false);
			}
		}
	}
}
