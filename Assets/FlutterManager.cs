using UnityEngine;
using System.Collections;

public class FlutterManager : MonoBehaviour {

	public GameObject flutterby;
	public GameObject flutterbyAndPartener;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool both = StateController.FlutterbyPartner;

		flutterby.SetActive(!both);
		flutterbyAndPartener.SetActive(both);
	}
}
