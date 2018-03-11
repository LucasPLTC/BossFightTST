using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveAluno2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionStay2D (Collision2D Gameover){
		if (Gameover.gameObject.name == "professor"){
			Application.LoadLevel ("gameover");
		}
	}
}
