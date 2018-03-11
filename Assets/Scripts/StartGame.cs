using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

	public AudioSource audio;
	public AudioClip prepareToFight;
	public AudioClip startbattle;
	public AudioClip buttonhc;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {

			Application.LoadLevel ("principalcena");
			audio.PlayOneShot (prepareToFight);
			audio.PlayOneShot (startbattle);
		}
		if(Input.GetKeyDown(KeyCode.H)){
			audio.PlayOneShot (buttonhc);
			Application.LoadLevel ("help");

			
		}
		if(Input.GetKeyDown(KeyCode.C)){
			audio.PlayOneShot (buttonhc);
			Application.LoadLevel ("New Scene");
			
		}
	}
}
