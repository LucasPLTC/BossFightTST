using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

	public AudioSource audio;
	public AudioClip prepareToFight;
	public AudioClip startbattle;


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
			Application.LoadLevel ("help");
			
		}
		if(Input.GetKeyDown(KeyCode.C)){
			Application.LoadLevel ("New Scene");
			
		}
	}
}
