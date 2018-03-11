using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveAluno1 : MonoBehaviour {

   public  Transform Player;
   
  



	// Use this for initialization
	void Start () {
        
      
        
	}
    void OnCollisionStay2D(Collision2D Gameover)
    {
        if (Gameover.gameObject.name == "professor")
        {
            Application.LoadLevel("gameover");
        }
        else
        {
            Player.position = Vector3.Reflect(Player.position, Vector3.right);
        }
    }


            // Update is called once per frame
            void Update () {

       

        

    }
	



	
	
}