using UnityEngine;
using System.Collections;

public class BaddyControl : SpriteControl {

	public GameObject convo;
	public GameObject sprite;
	
	public override void init() {
		locationLookup.Add(State.BaddyState, new Vector3(-9.4694f,-11.43127f,-2));
		convo.SetActive(false);
	}
	
	public override void update() {
		sprite.SetActive(StateController.CurrentState == State.FinalState || 
		                     StateController.CurrentState == State.BaddyState);
		convo.SetActive(StateController.CurrentState == State.BaddyState && firstLineSaid);
	}
	
	float timePassed = 0;
	bool firstLineSaid = false;
	bool what = false;
	bool dont = false;
	
	void OnGUI () {
		
		if(StateController.CurrentState != State.BaddyState || !atLocation()) {
			timePassed = 0;
			return;
		}
		
		if(!firstLineSaid && atLocation()) {
			if(GUI.Button(new Rect(Screen.width - 100,Screen.height - 40,80,20), "Oh look.")) {
				firstLineSaid = true;
			}
			return;
		}
		
		timePassed += Time.deltaTime;
		
		if(!what && !dont && timePassed > 5){
			if(GUI.Button(new Rect(Screen.width - 100,Screen.height - 40,80,20), "What?")) {
				what = true;
				timePassed = 0;
			}
		}

		if(what && !dont && timePassed > 1){
			if(GUI.Button(new Rect(Screen.width - 100,Screen.height - 40,80,20), "I don't...")) {
				dont = true;
				timePassed = 0;
			}
		}

		if(what && dont && timePassed > 1){
			if(GUI.Button(new Rect(Screen.width - 100,Screen.height - 40,80,20), "huh?")) {
				StateController.CurrentState = State.MenuState;
				dont = false;
				what = false;
				firstLineSaid = false;
				timePassed = 0;
			}
		}
	}
}
