using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn3 : MonoBehaviour {

	public GameObject alunotresPrefab;
	public float rateSpawn;
	public float currentTime;

	// Use this for initialization
	void Start () {

		currentTime = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		currentTime += Time.deltaTime;
		if (currentTime >= rateSpawn) {

			currentTime = 0;
			GameObject tempPrefab = Instantiate (alunotresPrefab) as GameObject;
			tempPrefab.transform.position = new Vector3 (Random.Range (-30, 30), Random.Range (-15, 13), tempPrefab.transform.position.z);
		}

		
	}
}
