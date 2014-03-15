using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum State{StartState, MenuState, DancefloorState, FlutterbyState, FlutterbyLeavesState,
					BlurrState, GState, GirlState, FlutterbyPartnerState, SaveState,
					FinalState, BaddyState};

public class StateController : MonoBehaviour {

	static Dictionary<State,StateValues> stateLookup = new Dictionary<State,StateValues>()
	{	
		{State.StartState, new StateValues(new Vector3(1.1f,2.5f,-10f), 0.0001f)},
		{State.MenuState, new StateValues(new Vector3(1.16f,2.39f,-10f), 2.8f)},
		{State.DancefloorState, new StateValues(new Vector3(-0.1f,-1.198f,-10f), 4.125f)},
		{State.FlutterbyState, new StateValues(new Vector3(-6.95f,-8.985f,-10), 4.125f)},
		{State.FlutterbyLeavesState, new StateValues(new Vector3(-6.95f,-8.985f,-10), 4.125f)},
		{State.BlurrState, new StateValues(new Vector3(15.34f,0.185f,-10), 4.125f)},
		{State.GState, new StateValues(new Vector3(7.303f,-8.995f,-10), 4.125f)},
		{State.GirlState, new StateValues(new Vector3(0.489f,-10.04f,-10), 4.125f)},
		{State.FlutterbyPartnerState, new StateValues(new Vector3(-0.5959389f,-10.04321f,-10), 4.125f)},
		{State.SaveState, new StateValues(new Vector3(-0.5959389f,-10.04321f,-10), 4.125f)},

		{State.FinalState, new StateValues(new Vector3(-0.1f,-1.198f,-10f), 4.125f)},
		{State.BaddyState, new StateValues(new Vector3(-3.86f,-13.98f,-10f), 5.2f)}
	};

	static Dictionary<Transition,TransitionValues> transitionsLookup = new Dictionary<Transition,TransitionValues>()
	{	
		{new Transition(State.StartState,State.MenuState), new TransitionValues(0.3f,0.43f)},
	};

	static State currentState;
	static Transition currentTransition;

	static HashSet<State> stateHistory = new HashSet<State>();
	static bool girl = false;
	static bool partners = false;

	static float timeInMenu = 0;

	public static State CurrentState {
		get { return currentState; }
		set { 
				if(value == State.MenuState)
					timeInMenu = 0;
				stateHistory.Add(currentState);
				if(stateHistory.Count > 2 && value == State.DancefloorState)
					girl = true;
				if(stateHistory.Contains(State.FlutterbyState) &&
				   stateHistory.Contains(State.BlurrState)
				   	&& value == State.DancefloorState)
					partners = true;
				if(stateHistory.Contains(State.SaveState) 
				   && stateHistory.Contains(State.GirlState) 
			   	   && value == State.DancefloorState) {
					currentState = State.FinalState;
				} else {
					if(value == State.MenuState) {
						stateHistory.Clear();
						girl = false;
						partners = false;
					}

					currentTransition = new Transition(currentState,value);
					currentState = value; 
				}
			}
	}

	public static bool SpokenToSomeone {
		get { return girl;}
	}

	public static bool FlutterbyPartner {
		get { return partners && !stateHistory.Contains(State.SaveState); }
	}

	public static Transition CurrentTransition {
		get { return currentTransition; }
	}

	public static bool GotToMenu {
		get { return timeInMenu > 7.52f; } 
	}

	public static bool BeenTo(State state) {
		return stateHistory.Contains(state);
	}

	// Use this for initialization
	void Start () {
		StateValues values = stateLookup[State.StartState];
		transform.position = values.viewLocation;
		camera.orthographicSize = values.viewSize;

		currentState = State.MenuState;
		currentTransition = new Transition(State.StartState,State.MenuState);

		stateHistory.Add(State.MenuState);
	}
	
	// Update is called once per frame
	void Update () {
		timeInMenu += Time.deltaTime;

		StateValues stateValues = stateLookup[currentState];

		TransitionValues transitionValues;
		if(transitionsLookup.ContainsKey(currentTransition)) 
			transitionValues = transitionsLookup[currentTransition];
		else
		    transitionValues = new TransitionValues(0.8f,0.5f);

		transform.position = Vector3.Slerp(transform.position,stateValues.viewLocation, Time.deltaTime* transitionValues.moveSpeed);
		camera.orthographicSize += (stateValues.viewSize-camera.orthographicSize)*Time.deltaTime*transitionValues.zoomSpeed;
	}
}

public class StateValues {
	public readonly Vector3 viewLocation;
	public readonly float viewSize;

	public StateValues(Vector3 viewLocation, float viewSize) {
		this.viewLocation = viewLocation;
		this.viewSize = viewSize;
	}
}

public class Transition {
	public readonly State from;
	public readonly State to;

	public Transition(State from, State to) {
		this.from = from;
		this.to = to;
	}

	public override bool Equals(object obj) {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((Transition) obj);
	}

	public bool Equals(Transition that)
	{
		return this.from.Equals(that.from) && this.to.Equals(that.to);
	}

	public override int GetHashCode()
	{
		return from.GetHashCode()+ 23*to.GetHashCode();
	}
}

public class TransitionValues {
	public readonly float moveSpeed;
	public readonly float zoomSpeed;

	public TransitionValues(float moveSpeed, float zoomSpeed) {
		this.moveSpeed = moveSpeed;
		this.zoomSpeed = zoomSpeed;
	}
}