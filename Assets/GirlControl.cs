using UnityEngine;
using System.Collections;

public class GirlControl : SpriteControl {
	public GameObject sprite;

	public override void init() {

	}

	public override void update() {
		sprite.SetActive(StateController.SpokenToSomeone);
	}
}
