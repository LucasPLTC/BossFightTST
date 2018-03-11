using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour {

	public AudioSource audio;
	public AudioClip rewind;
	public AudioClip startbattle;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {

			Application.LoadLevel ("principalcena");
			audio.PlayOneShot (rewind);
			audio.PlayOneShot (startbattle);
		}
	}
}
