using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cenario : MonoBehaviour
{

    private float Inicialspeed; //Velocidade de inicio.
    private float Nextspeed;

    private float tempo;
    private float tempoquerestou;

    private float nextActionTime;
    private float period;

    public Transform instantiatedGround; //Cria variável tipo Transform.

	private Vector3 movex;
	//private Vector3 colisao;






	void Start()
    {
        Inicialspeed = 5f;
        nextActionTime = 5.0f;
        period = 5.0f;
        Nextspeed = 1;
        tempo -= Time.time;
        tempoquerestou = Time.time;

    }


    void Update()
    {
        //OBS: Time.time é o tempo decorrido (em segundos) ao dar play no projeto;

        IncreaseSpeed();
        GroundMovement();
        GroundManager();

    }


    public void GroundMovement()
    {
        movex = new Vector3(-Inicialspeed * Time.deltaTime, 0, 0); //Cria variável "movex" do tipo Vector3, que guarda os componentes x,y e z.

        instantiatedGround.transform.Translate(movex); //Joga os valores x,y e z no Translate. Esse faz o cenario se movimentar.

    }

    public float IncreaseSpeed() //Função que aumenta velocidade
    {
        tempo = Time.time - (tempoquerestou-1);
        if (tempo >= nextActionTime)  //Quando o tempo decorrido for maior ou igual à variavel nextActionTime, ela executa oq tem dentro
        {
            nextActionTime += period;//Aumenta o valor do next action
            Inicialspeed += Nextspeed;

            //Não precisa apagar o comentario abaixo eheh
            /* InvokeRepeating("IncreaseSpeed", 10.0f * Time.deltaTime, 0.0f); *///Fução que espera X segundos para executar o método escolhido por X vezes
            
        }

        return Inicialspeed;

    }

    //Verifica se a posição x é < ou = a -10 e teleporta o chao pro inicio.
    void GroundManager()
    {
        if (instantiatedGround.position.x <= -10.0f)
        {
            instantiatedGround.position = new Vector3(60f, 0, 0);
        }
    }


}
