using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrinho : MonoBehaviour
{

  
    public float speed;
    public Transform CarrinhoInstanciado;
    public Cenario C;
    public Player player;


    //private int[] randomZ = new int[] {10, -10};//array com valores 2, 0, e -2


    // Start is called before the first frame update
    void Start()
    {
        C = FindObjectOfType<Cenario>();

        //randomZ[Random.Range(0, 2)]
        Vector3 Inicio = new Vector3(transform.position.x, transform.position.y, -10);
     
        CarrinhoInstanciado.transform.position = Inicio;


    }

  
    //carrinho se move
    void Update()
    {

        speed = C.IncreaseSpeed();

        //por algum motivo n é x, y e z, e sim z, y, x
        Vector3 movex = new Vector3(0, 0, speed * Time.deltaTime); //Cria variável "movex" do tipo Vector3, que guarda os componentes x,y e z.

        CarrinhoInstanciado.transform.Translate(movex); //Joga os valores x,y e z no Translate. Esse faz o carro se movimentar.


        Atropelar();
        ResetPosition();



    }



    void Atropelar()
    {

            Vector3 newPos = new Vector3(transform.position.x, transform.position.y, 10);

            if (CarrinhoInstanciado.position.x < Random.Range(3,10)) // Verifica se a posicao do objeto em x é menor que 3 no x global.
            {
                CarrinhoInstanciado.transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);
            }          

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Tirar vida do player
            player.vidaPlayer--;
           
             
        }

    }

    void ResetPosition()
    {
        if (CarrinhoInstanciado.position.x <= -10.0f)
        {
            CarrinhoInstanciado.position = new Vector3(60, 0.19f, -10);
        }
    }

    
}
