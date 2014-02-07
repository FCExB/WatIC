using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {

	public State stateOnClick = State.MenuState;
	public Color colorOnMouseOver = new Color(248/255f,184/255f,219/255f);

	SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseUpAsButton() {
		StateController.CurrentState = stateOnClick;
	}

	void OnMouseExit() {
		spriteRenderer.color = Color.white;
	}

	void OnMouseEnter() {
		if(StateController.CurrentState == State.DancefloorState)
			spriteRenderer.color = colorOnMouseOver; 
	}
}
