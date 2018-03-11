using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(CharacterController))]
public class AIPOINTS : MonoBehaviour
{ public Transform posi;
    public Transform dest;
    public Transform[] AIPoints; // PONTOS POR ONDE O INIMIGO PODE TRANSITAR
    private List<Transform> ListaDeAiPoints = new List<Transform>(); // LISTA QUE IRA GUARDAR OS AIPoints DISPONIVEIS PARA O INIMIGO ANDAR NO MOMENTO
    public float velocidadeDeMovimento = 5, DistanciaDeObservacao = 30, DistanciaDeSeguir = 20, DistanciaDeAtaque = 2; // VARIAVEIS A SEREM CONFIGURADAS
    private float distanciaDoJogador, cronometroTempoNoLocal;
    private int AIpoitAtual; // AIPoint ATUAL PARA ONDE O INIMIGO ESTA SE MOVENDDO
    private GameObject Player;
    private bool VendoOPlayer, EsperandoNoLocal, SeguindoOPlayer;
    public float TempoNoLocal = 0.001f; // TEMPO QUE O INIMIGO AGUARDA NO AIPoint ATE COMEÇAR A IR PARA O PROXIMO
    void Start()
    {
        Player = GameObject.FindWithTag("Player"); // PLAYER = OBJETO QUE CONTEM A TAG PLAYER
        for (int x = 0; x < AIPoints.Length; x++)
        { // AQUI EU FAÇO UM FOR EM TODOS OS OBJETOS DA VARIAVEL AIPoints
            Vector3 de = transform.position; // A VARIAVEL " de " RECEBE A POSIÇAO ATUAL DO INIMIGO
            Vector3 para = AIPoints[x].transform.position; // A VARIAVEL " para " RECEBE A POSIÇAO DO AIpointAtual
            if (!Physics.Linecast(de, para))
            { // AQUI EU CHEGO SE O CAMINHO ENTRE O INIMIGO E O AIPOINT ESTA LIVRE, SE ESTIVER:
                if (!ListaDeAiPoints.Contains(AIPoints[x]))
                { // AQUI EU CHECO SE O ELEMENTO JA ESTA NA LISTA, SE NAO ESTA, FAZ ISSO ABAIXO:
                    ListaDeAiPoints.Add(AIPoints[x]); //ADICIONA O AIPoint[X] NA LISTA
                }
            }
            else if (Physics.Linecast(de, para))
            { // SEM TEM ALGUMA COISA ENTRE O AIPoint ATUAL E O INIMIGO
                ListaDeAiPoints.Remove(AIPoints[x]); // REMOVE O ITEM AIPoint[X] ATUAL DA LISTA, PARA TORNA-LO INDISPONIVEL
            }
        }
    }
    void Update()
    {
        //================================================RAYCAST ( INIMIGO ESTA VENDO O PLAYER? )=========================================================//
        RaycastHit hit; // DECLARO UM RAYCAST
        Vector3 deOnde = transform.position; //A VARIAVEL deOnde RECEBE A POSIÇAO ATUAL DO INIMIGO
        Vector3 paraOnde = Player.transform.position;//A VARIAVEL paraOnde RECEBE A POSIÇAO ATUAL DO INIMIGO
        Vector3 direction = paraOnde - deOnde; // O VETOR direction RECEBE A POSIÇAO CORRESPONDENTE A B-A, OU SEJA, O VETOR paraOnde - o vetor deOnde
        if (Physics.Raycast(transform.position, direction, out hit, 1000) && distanciaDoJogador < DistanciaDeObservacao)
        { // SE RAYCAST, E SE A DISTANCIA DO PLAYER FOR MENOR DO QUE A MAXDISTANCIA
            if (hit.collider.gameObject.CompareTag("Player"))
            { // SE A TAG DO OBJETO QUE O RAYCAST COLIDIU FOR "PLAYER"
                VendoOPlayer = true; // VARIAVEL VendoOPlayer FICA VERDADEIRA
            }
            else
            { // SE NAO 
                VendoOPlayer = false;// VARIAVEL VendoOPlayer FICA FALSA
            }
        }
        //===========================================LINECAST ( DEIXAR ACESSIVEL APENAS OS AIPOINTS VISIVEIS)=========================================//
        if (EsperandoNoLocal == true)
        { // SE A VARIAVEL EsperandoNoLocal ESTIVER VERDADEIRA
            for (int x = 0; x < AIPoints.Length; x++)
            { // AQUI EU FAÇO UM FOR EM TODOS OS OBJETOS DA VARIAVEL AIPoints
                Vector3 de = transform.position; // A VARIAVEL " de " RECEBE A POSIÇAO ATUAL DO INIMIGO
                Vector3 para = AIPoints[x].transform.position; // A VARIAVEL " para " RECEBE A POSIÇAO DO AIpointAtual
                if (!Physics.Linecast(de, para))
                { // AQUI EU CHEGO SE O CAMINHO ENTRE O INIMIGO E O AIPOINT ESTA LIVRE, SE ESTIVER:
  //                  Debug.DrawLine(de, para); // APENAS FAÇO UM DEBUG ( OPCIONAL ) PARA VER A TRAJETORIA DAS LINHAS
                    if (!ListaDeAiPoints.Contains(AIPoints[x]))
                    { // AQUI EU CHECO SE O ELEMENTO JA ESTA NA LISTA, SE NAO ESTA, FAZ ISSO ABAIXO:
                        ListaDeAiPoints.Add(AIPoints[x]); //ADICIONA O AIPoint[X] NA LISTA
                    }
                }
                else if (Physics.Linecast(de, para))
                { // SEM TEM ALGUMA COISA ENTRE O AIPoint ATUAL E O INIMIGO
   //                 Debug.DrawLine(de, para);// APENAS FAÇO UM DEBUG ( OPCIONAL ) PARA VER A TRAJETORIA DAS LINHAS
                    ListaDeAiPoints.Remove(AIPoints[x]); // REMOVE O ITEM AIPoint[X] ATUAL DA LISTA, PARA TORNA-LO INDISPONIVEL
                }
            }
            EsperandoNoLocal = false; // A VARIAVEL EsperandoNoLocal FICA FALSA
        }
        //==================================================== COMANDO PARA FAZER O INIMIGO ANDAR PELOS AIPOINTS======================================//
        distanciaDoJogador = Vector3.Distance(Player.transform.position, transform.position); // MEDE A DISTANCIA ENTRE O INIMIGO E O PLAYER
        if (distanciaDoJogador >= DistanciaDeObservacao || VendoOPlayer == false && EsperandoNoLocal == false)
        { // SE A DISTANCIA ENTRE INIMIGO E PLAYER FOR MENOR DO QUE A DISTANCIA DE OBSERVAÇAO E VAR'S....
            if (AIpoitAtual < ListaDeAiPoints.Count)
            { // CHECAGEM PRA NAO DAR ERRO DE ARRAY
                Vector3 target = ListaDeAiPoints[AIpoitAtual].transform.position;//A VARIAVEL target RECEBE A POSIÇAO DO AIpointAtual
                transform.TransformDirection(target);//O INIMIGO OLHA NA DIREÇAO DO AIPOINT SELECIONADO ( O AIpointAtual )
                transform.position = Vector3.MoveTowards(transform.position, target, velocidadeDeMovimento * Time.deltaTime); // MOVE O PALYER EM DIREÇAO AO AIpointAtual
                if (transform.position == ListaDeAiPoints[AIpoitAtual].transform.position)
                {
                            // SE A POSIÇAO DO PLAYER FOR IGUAL A POSIÇAO DO AIpointAtual, OU SEJA, CHEGOU NO DESTINO
                             cronometroTempoNoLocal += Time.deltaTime; // COMEÇA A CONTAR O CRONOMETRO DE ESPERA NO LOCAL
                             EsperandoNoLocal = true; // A VARIAVEL EsperandoNoLocal FICA VERDADEIRA PARA INDICAR QUE ESTA ESPERANDO
                        }
                if (cronometroTempoNoLocal >= TempoNoLocal)
                { // SE JA ESPEROU O TEMPO QUE DEVERIA ESPERAR NO LOCAL
                    cronometroTempoNoLocal = 0; // ZERA O CRONOMETRO PARA PODER REUTILIZAR ELE
                    int NumeroDeElementosDaLista2 = ListaDeAiPoints.Count; // UMA INT COM NOME NumeroDeElementosDaLista2 RECEBE O NUMERO DE ITENS QUE TEM NA LISTA
                    AIpoitAtual = Random.Range(0, NumeroDeElementosDaLista2); // A VARIAVEL AIpoitAtual RECEBE UM VALOR ALEATORIO ENTRE 0 E O O NUMERO DE ITENS QUE TEM NA LISTA
                }
            }
        }
        //================================================= COMANDOS PARA CHECAR DISTANCIA, OLHAR E SEGUIR================================================//
        else if (distanciaDoJogador >= DistanciaDeSeguir && distanciaDoJogador < DistanciaDeObservacao && VendoOPlayer == true)
        { // SE ESTA PERTO PARA OLHAR MAS AINDA E LONGE PARA SEGUIR:
            Olhar(); // VOID PARA O INIMIGO FICAR OLHANDO PARA O PLAYER
        }
        else if (distanciaDoJogador <= DistanciaDeSeguir && VendoOPlayer == true)
        {  //SE JA ESTA PERTO O SUFICIENTE PARA SEGUIR E ESTA VENDO O PLAYER
            Seguir();  // VOID PARA O INIMIGO SEGUIR O PLAYER 
        }
        //=======================================================CORRIGIR BUGS DE INDEX RANGE=============================================================//
        if (AIpoitAtual >= ListaDeAiPoints.Count)
        { // SE O AIpoitAtual TIVER UM VALOR MAIOR DO QUE O MAXIMO DE ELEMENTOS QUE EXISTEM NA LISTA
            AIpoitAtual = ListaDeAiPoints.Count - 1; // A VARIAVEL AIpoitAtual RECEBE O NUMERO DE ELEMENTOS DA LISTA -1, ISSO EVITARA BUGS
        }
        else if (AIpoitAtual <= 0)
        { //SE O AIpoitAtual TIVER UM VALOR MENOR DO QUE ZERO:
            AIpoitAtual = 0;//AIpoitAtual RECEBERA O VALOR ZERO, ISSO EVITARA BUGS
        }
        //==========================================FAZER O PLAYER RECEBER DANO AO CHEGAR PERTO DO INIMIGO================================================//
        
        //===============================================ESTAVA SEGUINDO MAS PAROU DE VER O PLAYER========================================================//
        if (SeguindoOPlayer == true && VendoOPlayer == false)
        { // SE ESTA SEGUINDO O PLAYER MAS NAO ESTA VENDO ELE
            for (int x = 0; x < AIPoints.Length; x++)
            { // AQUI EU FAÇO UM FOR EM TODOS OS OBJETOS DA VARIAVEL AIPoints
                Vector3 de = transform.position;  // A VARIAVEL " de " RECEBE A POSIÇAO ATUAL DO INIMIGO
                Vector3 para = AIPoints[x].transform.position;  // A VARIAVEL " para " RECEBE A POSIÇAO DO AIpointAtual
                if (!Physics.Linecast(de, para))
                { // AQUI EU CHEGO SE O CAMINHO ENTRE O INIMIGO E O AIPOINT ESTA LIVRE, SE ESTIVER:
 //                   Debug.DrawLine(de, para); // APENAS FAÇO UM DEBUG ( OPCIONAL ) PARA VER A TRAJETORIA DAS LINHAS
                    if (!ListaDeAiPoints.Contains(AIPoints[x]))
                    { // AQUI EU CHECO SE O ELEMENTO JA ESTA NA LISTA, SE NAO ESTA, FAZ ISSO ABAIXO:
                        ListaDeAiPoints.Add(AIPoints[x]); //ADICIONA O AIPoint[X] NA LISTA
                    }
                }
                else if (Physics.Linecast(de, para))
                { // SEM TEM ALGUMA COISA ENTRE O AIPoint ATUAL E O INIMIGO
 //                   Debug.DrawLine(de, para);// APENAS FAÇO UM DEBUG ( OPCIONAL ) PARA VER A TRAJETORIA DAS LINHAS
                    ListaDeAiPoints.Remove(AIPoints[x]); // REMOVE O ITEM AIPoint[X] ATUAL DA LISTA, PARA TORNA-LO INDISPONIVEL
                }
            }
            int NumeroDeElementosDaLista2 = ListaDeAiPoints.Count; // A VARIAVEL NumeroDeElementosDaLista2 RECEBE O NUMERO DE ELEMENTOS CONTIDOS NA LISTA
            AIpoitAtual = Random.Range(0, NumeroDeElementosDaLista2); // A VARIAVEL AIpoitAtual RECEBE UM VALOR ALEATORIO ENTRE 0 E O O NUMERO DE ITENS QUE TEM NA LISTA
                                                                      // SeguindoOPlayer = false; // A VARIAVEL SeguindoOPlayer RECEBE FALSE

        }
         
         

    }


    //================================================================= OUTRAS VOIDS=======================================================================//
    void Olhar()
    { // VOID QUE FAZ O INIMIGO OLHAR PARA O PLAYER
        Vector3 OlharPlayer = Player.transform.position; // O VETOR OlharPlayer RECEBE A POSIÇAO DO PLAYER
        transform.TransformDirection(OlharPlayer); // O INIMIGO PLHA EM DIREÇAO AO PLAYER
    }
    void Seguir()
    {
        SeguindoOPlayer = true; // A VARIAVEL SeguindoOPlayer FICA VERDADEIRA
        Vector3 SeguirPlayer = Player.transform.position; // O VETOR SeguirPlayer RECEBE A POSIÇAO DO PLAYER
        transform.position = Vector3.MoveTowards(transform.position, SeguirPlayer, velocidadeDeMovimento * Time.deltaTime); // O INIMIGO SE MOVE EM DIREÇAO AO PLAYER
    }
}