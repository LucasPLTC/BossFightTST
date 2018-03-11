using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveAluno1 : MonoBehaviour {

	public Rigidbody2D alunoumRigidBody2D;
	public float velocidadealunoum;
	private ArrayList vetorInimigo = new ArrayList();
	public int i;
	public float cronometro;
    public GameObject professor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        professor = GameObject.Find("professor");

		for (i=0; i<2; i++){
			cronometro += 1*Time.deltaTime;
		}
		if (cronometro >=2){
			vetorInimigo.Add (professor.transform.position);
		}
		cronometro = 0;
	}
}
