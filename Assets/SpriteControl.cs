using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class SpriteControl : MonoBehaviour {

	public float defaultMoveSpeed = 0.8f;
	public Vector3 defaultLocation = new Vector3();

	protected Dictionary<State,Vector3> locationLookup = new Dictionary<State,Vector3>();
	protected Dictionary<Transition,float> speedLookup = new Dictionary<Transition,float>();
	
	// Use this for initialization
	void Start () {
		init();
	}

	public abstract void init();
	public abstract void update();
	
	// Update is called once per frame
	void Update () {
		Vector3 location;
		if(locationLookup.ContainsKey(StateController.CurrentState))
			location = locationLookup[StateController.CurrentState];
		else 
			location = defaultLocation;

		float moveSpeed;
		if(speedLookup.ContainsKey(StateController.CurrentTransition))
			moveSpeed = speedLookup[StateController.CurrentTransition];
		else 
			moveSpeed = defaultMoveSpeed;
		
		transform.position = Vector3.Lerp(transform.position,location, Time.deltaTime*moveSpeed);

		update();
	}

	public bool atLocation() {
		Vector3 location;
		if(locationLookup.ContainsKey(StateController.CurrentState))
			location = locationLookup[StateController.CurrentState];
		else 
			location = defaultLocation;

		return Vector3.Magnitude(transform.position - location) < 0.3f;
	}
}
