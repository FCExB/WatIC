using UnityEngine;
using System.Collections;

public class BaddyControl : SpriteControl {

	public GameObject convo;
	public GameObject sprite;
	
	public override void init() {
		locationLookup.Add(State.BaddyState, new Vector3(-9.4694f,-11.43127f,-2));
		locationLookup.Add(State.TogetherState, new Vector3(-7f,-11.43127f,-2));
		convo.SetActive(false);
	}
	
	public override void update() {
		sprite.SetActive(StateController.ShowBaddy);
		convo.SetActive((StateController.CurrentState == State.BaddyState || 
		                 StateController.CurrentState == State.TogetherState) 
		                	&& firstLineSaid);
	}
	
	float timePassed = 0;
	bool firstLineSaid = false;

	int state = 0;
	
	void OnGUI () {
		
		if(!(StateController.CurrentState == State.BaddyState || 
		     StateController.CurrentState == State.TogetherState) || !atLocation()) {
			timePassed = 0;
			return;
		}
		
		if(!firstLineSaid && atLocation()) {
			if(GUI.Button(new Rect(Screen.width*0.2f,Screen.height*0.5f,
			                       Screen.width*0.15f,Screen.height*0.08f), "Oh look.")) {
				firstLineSaid = true;
			}
			return;
		}
		
		timePassed += Time.deltaTime;
		
		if(state == 0 && timePassed > 5){
			if(GUI.Button(new Rect(Screen.width*0.8f,Screen.height*0.75f,
			                       Screen.width*0.15f,Screen.height*0.08f), "What?")) {
				StateController.CurrentState = State.TogetherState;
				state++;
				timePassed = 0;
			}
		}

		if(state == 1 && timePassed > 2){
			if(GUI.Button(new Rect(Screen.width*0.4f,Screen.height*0.4f,
			                       Screen.width*0.2f,Screen.height*0.2f), "That doesn't...")) {
				state++;
				timePassed = 0;
			}
		}

		if(state == 2 && timePassed > 2){
			if(GUI.Button(new Rect(Screen.width*0.3f,Screen.height*0.3f,
			                       Screen.width*0.4f,Screen.height*0.4f), "I don't...")) {
				state++;
				timePassed = 0;
			}
		}

		if(state == 3 && timePassed > 2){
			if(GUI.Button(new Rect(Screen.width*0.15f,Screen.height*0.15f,
			                       Screen.width*0.7f,Screen.height*0.7f), "huh?")) {
				StateController.CurrentState = State.MenuState;
				state = 0;
				firstLineSaid = false;
				timePassed = 0;
			}
		}
	}
}
