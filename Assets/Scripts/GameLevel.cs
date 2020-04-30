using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevel : MonoBehaviour
{
    //Esse script é para fazer transição entre cenas ou level.
    //As funções aqui são os botões do jogo.
    public Cenario C;

    //Paineis do menu do jogo
    public GameObject painelMENU;
    public GameObject painelINFO;
	public GameObject painelSetting;


    public int lvl1Score= 20;
    public int lvl2Score = 30;
    public int lvl3Score = 40;
    public int lvl4Score = 50;
    public int lvl5Score = 60;
    public int cenaAtual;



    void Start()
    {
        Time.timeScale = 1;
        C = FindObjectOfType<Cenario>();
        cenaAtual = SceneManager.GetActiveScene().buildIndex;
    }


    void Update()
    {
       ScoreGoal(); 
    }

    public int ScoreGoal()//retorna o score necessário para passar do level atual.
    {
        if (cenaAtual == 0) //menu
        {
            return 0;

        }else if (cenaAtual == 1) //l1
        {
            return 20;
            
        }else if (cenaAtual == 2) //l2
        {
            return 30;
        } else if (cenaAtual == 3) //l3
        {
            return 40;
        }
        else if (cenaAtual == 4)//l4
        {
            return 50;
        }
        else if (cenaAtual == 5) //l5
        {
            return 60;
        }
        else
        {
            return 0;
        }
        
    }

    public void BtnTrocaLevel()
    {

        cenaAtual++;

        if (cenaAtual > 5)
        {
            cenaAtual = 0;
            SceneManager.LoadScene(cenaAtual);
        }
        else
        {
            SceneManager.LoadScene(cenaAtual);
        }

        
    }

    public void BtnExit()
    {
        Application.Quit();
    }

    public void BtnVoltaMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void BtnSTART()
    {
        SceneManager.LoadScene("Level1");
    }

    public void BtnINFO()
    {
		painelINFO.SetActive(true);
		painelMENU.SetActive(false);
		painelSetting.SetActive(false);
	}

    public void BtnReturn()
    {
        painelMENU.SetActive(true);
        painelINFO.SetActive(false);
		painelSetting.SetActive(false);
	}

	public void BtnSetting()
	{
		painelMENU.SetActive(false);
		painelINFO.SetActive(false);
		painelSetting.SetActive(true);

	}

}
