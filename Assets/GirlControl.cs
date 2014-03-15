﻿using UnityEngine;
using System.Collections;

public class GirlControl : SpriteControl {
	
	public GameObject convo;
	public GameObject sprite;

	public override void init() {
		locationLookup.Add(State.GirlState, new Vector3(-3.37f,-9.98f,-2));
		locationLookup.Add(State.FinalState, new Vector3(-0.39f,-2.95f,-2));
		locationLookup.Add(State.BaddyState, new Vector3(-4.95f,-11.24f,-2));
		locationLookup.Add(State.TogetherState, new Vector3(-6f,-11.24f,-2));
		convo.SetActive(false);
	}

	public override void update() {
		sprite.SetActive(StateController.SpokenToSomeone);
		convo.SetActive(StateController.CurrentState == State.GirlState && firstLineSaid);
	}

	float timePassed = 0;
	bool firstLineSaid = false;

	void OnGUI () {

		if(StateController.CurrentState != State.GirlState || !atLocation()) {
			timePassed = 0;
			return;
		}
		
		if(!firstLineSaid && atLocation()) {
			if(GUI.Button(new Rect(Screen.width*0.65f,Screen.height*0.07f,
			                       Screen.width*0.1f,Screen.height*0.08f), "Hi")) {
				firstLineSaid = true;
			}
			return;
		}

		timePassed += Time.deltaTime;

		if(timePassed > 5){
			if(GUI.Button(new Rect(Screen.width*0.55f,Screen.height*0.9f,
			                       Screen.width*0.15f,Screen.height*0.08f), "*leave*")) {
				StateController.CurrentState = State.DancefloorState;
				firstLineSaid = false;
			}
		}
	}
}
