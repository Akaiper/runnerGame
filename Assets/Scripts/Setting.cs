using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
	public Slider Music;
	public Slider SFX;
	public AudioConfig audioConfig;

    // Start is called before the first frame update
    void Start()
    {
		//Music.value = audioConfig.music;
		//SFX.value = audioConfig.sfx;
    }

    // Update is called once per frame
    void Update()
    {
		audioConfig.music = Music.value;
		audioConfig.sfx = SFX.value;
    }
}
