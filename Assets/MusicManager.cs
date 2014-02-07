using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip first;
	public AudioClip second;
	
	private bool firstsGo = true;

	// Use this for initialization
	void Start () {
		audio.clip = first;

	}
	
	// Update is called once per frame
	void Update () {
		if(firstsGo && !audio.isPlaying)
		{
			audio.Play();
		}

		if(!firstsGo && !audio.isPlaying)
		{
			audio.clip = second;
			audio.loop = true;
			
			audio.Play();
		}
	}
}
