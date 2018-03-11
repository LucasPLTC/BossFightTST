using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn1 : MonoBehaviour {

	public GameObject alunoumPrefab; // objeto a ser spawnado
	public float rateSpawn;			// intervalo de spawn
	public float currentTime;
	public AudioSource audio;
	public AudioClip spawnsound;
	// Use this for initialization
	void Start () {
		
		currentTime = 0;

	}
	
	// Update is called once per frame
	void Update () {

		currentTime += Time.deltaTime;  //deltaTime conta o tempo entre os frames. isso é usado pra não haver diferença de tempo de spawn entre um PC pica a 80km/h e um bostinha
		if (currentTime >= rateSpawn) {
			currentTime = 0;
			GameObject tempPrefab = Instantiate (alunoumPrefab) as GameObject;
			audio.PlayOneShot (spawnsound);
			tempPrefab.transform.position = new Vector3 (Random.Range(-30,30), Random.Range(-15,12), tempPrefab.transform.position.z);

		}
		
	}
}
