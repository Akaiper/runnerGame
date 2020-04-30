﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculos : MonoBehaviour
{
    public float speed;
    public GameObject b;
    public Transform ObstaculoInstanciado; //Cria uma variável do tipo Transform, da biblioteca da Unity.
    private readonly int[] positions = new int[] { 2, 0, -2 };//array com valores 2, 0, e -2
    public Cenario C;
    public Player player;




   
    void Start()
    {
        player = FindObjectOfType<Player>();
        C = FindObjectOfType<Cenario>();
        //Inicia o coletável dentro dessas posições x, y e z.
        ObstaculoInstanciado.transform.Translate(60f, 0f, positions[Random.Range(0, 3)]);//Ranom.Range vai escolher slot do array pra fazer tipo um "random" na posição z entre os valores 2, 0 e -2

    }

   
    void Update()
    {
        speed = C.IncreaseSpeed();

        Vector3 movex = new Vector3(-speed * Time.deltaTime, 0, 0); //Cria variável do tipo Vector3, na qual guarda valores para os componentes x, y e z. O nome da variável Vector3, nesse caso, é "movex"

        //O código abaixo que faz o objeto realmente se mover.
        ObstaculoInstanciado.transform.Translate(movex); //Como o "ColetavelInstanciado" é do tipo Transform, ele suporta variáveis Vector3, logo, dá pra colocar o "movex" nos parênteses.

        if (ObstaculoInstanciado.position.x <= -10f)
        {

            ObstaculoInstanciado.position = new Vector3(60f, 0f, positions[Random.Range(0, 3)]); //Faz o coletável teleportar pro inicio novamente ao sair do mapa
        }
    }




    private void OnTriggerEnter(Collider other)
    {


       //fazer dar dano no player
       if (other.gameObject.CompareTag("Player"))
       {
            player.vidaPlayer--;
		}
    }
}
