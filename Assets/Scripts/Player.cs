using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public Rigidbody rbPlayer; //Variável do tipo RigidBody
    public GameObject GOplayer;
    public int pista; //Player pode navegar entre 3 "pistas"
    private int score; //Pontuação atual
    public int totalscore;//Pontuação que ele tem que chegar no level correspondente, para assim passar do level
	private float secTempo;
	private int sec = 0;
	private float minTempo;
	public TextMeshProUGUI scoreTEXT; //Variável do tipo Text  //Variável para guardar o índice da Cena/Level atual
    public TextMeshProUGUI vidaTEXT;
	public TextMeshProUGUI timeTEXT;
    public GameObject painelNextLevel;//Painel que apenas aparece após terminar o level, e permite que avançe de level.
    public GameObject painelGameOver;
    public GameLevel GL; //Criou slot GL para o script de GameLevel
    //Vector3 originalpos;
    private readonly float jumpvelocity = 5;
    public int vidaPlayer = 10;
    public AudioClip jumpsound;
    public AudioClip hurtsound;
    public AudioClip powerupsound;
	public AudioConfig audioConfig;
    Quaternion currentRotation;





    void Start() //Só executa ao iniciar a cena.
    {

		audioConfig = Resources.Load<AudioConfig>("Audio_Config");

		vidaPlayer = 10;
        //Iniciando variaveis
        Time.timeScale = 1;
        score = 0;
        totalscore = 0;
        pista = 1;
		minTempo = 0; //Pega o minuto inicial de quando o jogo começa e passa para int
		secTempo = 0;//Pega os segundos iniciais de quando o jogo começa e passa para int

		//originalpos = rbPlayer.transform.position; //Guarda a posição de início do player.

		GL = FindObjectOfType<GameLevel>(); //Atribui o script no slot GL

        painelNextLevel.SetActive(false); //Painel de Next Level inicia inativo.

        rbPlayer = GetComponent<Rigidbody>(); //Puxa o componente RigidBody automaticamente.
                                              //SceneManager é um comando da biblioteca "UnityEngine.SceneManagement"

        AtualizaTextos();

        currentRotation = transform.rotation;
    }


    void Update()
    {

        Debug.Log(EstaNoChao());//mostra no console se esta retornando true ou false.
		Debug.Log(Time.time);

        PlayerControl();
        AtualizaTextos();
        WinLevel();
        VerificaVida();
		Cronometro();

        //Fica corrigindo rotação do player pra ele não cair

        Quaternion wantedRotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation, Time.deltaTime * 1);


        Vector3 position = transform.position;
        position[0] = 0.49f; // the Z value
        transform.position = position;
        //nao deixa nennhum objeto empurrar ele em x


    }

    bool EstaNoChao()
    {
        return Physics.Raycast(GOplayer.transform.position, Vector3.down, 0.9f); //retorna true se esta no chao ou false se nao estiver
    }



    public void PlayerControl()
    {
        
      
        //Testa qual botão está pressionado e muda a pista.
        //As pistas são: 0=Esquerda, 1=Meio, 2=Direita, o Start começa com a pista=1, ou seja, o player no meio da tela.
       
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pista -= 1;
        } 
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            pista += 1;
        }

        if (Input.GetKey(KeyCode.Space)&& EstaNoChao() == true) //so pula se chao for true
        {
 
            //rbPlayer.AddForce(Vector3.up * jumphight);
            Vector3 jump = new Vector3(0f, jumpvelocity, 0f);
            rbPlayer.velocity = jump;
            AudioSource.PlayClipAtPoint(jumpsound, this.transform.position, audioConfig.sfx);
			
        }


        //Impede que os valores da pista sejam maior que zero ou maior que 1.
        if (pista <= 0)
        {
            pista = 0;
        }
        if (pista >= 2)
        {
            pista = 2;
        }

        Vector3 newPosEsquerda = new Vector3(transform.position.x, transform.position.y, 2);
        Vector3 newPosMeio = new Vector3(transform.position.x, transform.position.y, 0);
        Vector3 newPosDireita = new Vector3(transform.position.x, transform.position.y, -2);

        //Aqui faz o player realmente se movimentar para a pista correspondente.
        switch (pista)
        {
           
            case 0://esquerda

                rbPlayer.transform.position = Vector3.Lerp(transform.position, newPosEsquerda, Time.deltaTime*5);
                break;

            case 1://meio
                rbPlayer.transform.position = Vector3.Lerp(transform.position, newPosMeio, Time.deltaTime * 5);
                break;

            case 2://direita
                rbPlayer.transform.position = Vector3.Lerp(transform.position, newPosDireita, Time.deltaTime * 5);
                break;
                           
        }

       

    }

    //Testa colisão com colecionável e atribui score.
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coletavel"))
        {
            score += 1;
        } else if (other.gameObject.CompareTag("Obstaculo")) {
            AudioSource.PlayClipAtPoint(hurtsound, this.transform.position, audioConfig.sfx);

        } else if (other.gameObject.CompareTag("PowerUp"))
        {
            AudioSource.PlayClipAtPoint(powerupsound, this.transform.position, audioConfig.sfx);
            
        }


    }



    //Atualiza o TEXTO score.
    void AtualizaTextos()
    {      
        scoreTEXT.text = "Score: " + score.ToString() + " / " + GL.ScoreGoal();
        vidaTEXT.text = "Vida: " + vidaPlayer.ToString() + " / 10";
		timeTEXT.text = "Tempo: " + minTempo.ToString() + ":" + sec.ToString();
    }

    
    //Testa se o player conseguiu pegar todos os colecionáveis e chama o painel de Next Level.
    void WinLevel()
    {
        totalscore = GL.ScoreGoal();

        if (score >= totalscore)
        {
            Time.timeScale = 0.0000000000000000000000000000000000000000000000000000000000000000000000001f; //Faz o jogo pausar.
            painelNextLevel.SetActive(true); //Faz o painel ficar ativo
        }

    }

    //Chama painel GAMEOVER
    void VerificaVida()
    {
        if (vidaPlayer <= 0)
        {
            Time.timeScale = 0.0000000000000000000000000000000000000000000000000000000000000000000000001f;
            painelGameOver.SetActive(true);

        }
    }
	
	void Cronometro()
	{
		secTempo += Time.deltaTime;
		sec = (int)secTempo;

		if (secTempo >= 60)
		{
			secTempo = 0;
			minTempo += 1;
		}
	
	}
}

