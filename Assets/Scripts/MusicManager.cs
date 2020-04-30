using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public GameLevel GM;
	//Gerenciador da musica do level

	public AudioConfig audioConfig;
    public AudioSource music;
    public AudioClip menu;
    public AudioClip L1;
    public AudioClip L2;
    public AudioClip L3;
    public AudioClip L4;
    public AudioClip L5;

    public static MusicManager instance = null;

    void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
		audioConfig = Resources.Load<AudioConfig>("Audio_Config");
        
        if (instance == null)
        {
                instance = this;
                DontDestroyOnLoad(gameObject);
        }
		else 
		{ 
                Destroy(gameObject);
        }

        OnLevelWasLoaded(GM.ScoreGoal());

    }

    // Update is called once per frame
    void Update()
    {
		music.volume = audioConfig.music;

    }


    void OnLevelWasLoaded(int level)
    {
        switch (level)
        {
            case 0:
                music.loop = true;
                music.clip=menu;
                music.Play();
       break;

            case 1:
            music.Stop();
                music.loop = true;
                music.clip = L1;
                music.Play();
                break;

            case 2:
            music.Stop();
                music.loop = true;
                music.clip = L2;
                music.Play();
                break;

            case 3:
            music.Stop();
                music.loop = true;
                music.clip =  L3;
                music.Play();
                break;

            case 4:
            music.Stop();
                music.loop = true;
                music.clip = L4;
                music.Play();
                break;

            case 5:
            music.Stop();
                music.loop = true;
                music.clip = L5;
                music.Play();
                break;

        }
       

    }
}
