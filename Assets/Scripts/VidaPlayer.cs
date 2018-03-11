using UnityEngine;
using System.Collections;
public class VidaPlayer : MonoBehaviour

{
    public Rigidbody2D playerRigidBody2D;
    public float velocidade;

	//PONTUAÇÃO
    public static int pontuacao;
    public UnityEngine.UI.Text txtPontos;

    public Transform Player;
    public static bool RecebendoDano; //VARIAVEL ESTATICA QUE INDICA SE ESTA RECEBENDO DANO OU NAO
    public float tempoPorAtaque = 1.5f; // TEMPO MINIMO ENTRE CADA ATAQUE QUE O INIMIGO PODE DAR
    private float cronometroDeAtaque; // CRONOMETRO QUE CONTROLA O TEMPO DOS ATAQUES
    public int VidaDoPlayer = 100; // VIDA DO PERSONAGEM
    public int DanoPorAtaque = 40;
	public int i;


    void Start()
    {
        RecebendoDano = false; // A VARIAVEL RecebendoDano RECEBE FALSO
		pontuacao = 0;
		PlayerPrefs.SetInt ("pontuacao", pontuacao);
		i = 0;
    }

    void Update()
        
    {
        playerRigidBody2D.velocity = velocidade * ((Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 2f) - transform.position);
		txtPontos.text = pontuacao.ToString ();
		i = i + 1;
		if (i == 60) {
			VidaPlayer.pontuacao += 1;
			i = 0;
		}
        if (RecebendoDano == true)
        { // SE RecebendoDano ESTA VERDADEIRO
            cronometroDeAtaque += Time.deltaTime; // O CRONOMETRO COMEÇA A CONTAR
        }
        else
        { // SE NAO, OU SEJA, SE RecebendoDano ESTA FALSO
            cronometroDeAtaque = 0; // O CRONOMETRO RECEBE 0
        }
        if (cronometroDeAtaque >= tempoPorAtaque)
        { // SE O CRONOMETRO ULTRAPASSOU O TEMPO POR ATAQUE
            cronometroDeAtaque = 0; // CRONOMETRO RECEBE 0
            VidaDoPlayer = VidaDoPlayer - DanoPorAtaque; // A VIDA DO PLAYER RECEBE O VALOR DELA MESMA MENOS O DANO DO ATAQUE
        }
        if (VidaDoPlayer <= 0)
        { // SE A VIDA FOR MENOR OU IGUAL A 0
            Application.Quit(); // SAI DO JOGO
        }

			PlayerPrefs.SetInt ("pontuacao", pontuacao);
			if(pontuacao > PlayerPrefs.GetInt("recorde")){
				PlayerPrefs.SetInt ("recorde", pontuacao);
			}

    }
}