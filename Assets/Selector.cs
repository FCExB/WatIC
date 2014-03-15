using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {

	public State stateOnClick = State.MenuState;
	public Color colorOnMouseOver = new Color(248/255f,184/255f,219/255f);

	Color defaultColor;

	SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		defaultColor = spriteRenderer.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseUpAsButton() {
		if(StateController.CurrentState == State.DancefloorState &&
		   	!StateController.BeenTo(stateOnClick))
			StateController.CurrentState = stateOnClick;
	}

	void OnMouseExit() {
		spriteRenderer.color = defaultColor;
	}

	void OnMouseEnter() {
		if(StateController.CurrentState == State.DancefloorState && 
		   	!StateController.BeenTo(stateOnClick))
			spriteRenderer.color = colorOnMouseOver; 
	}
}
