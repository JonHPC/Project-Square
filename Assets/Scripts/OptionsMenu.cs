using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    public AudioSource music;
    public AudioSource sfx;

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.5f); // stores ths slider value to the player pref musicVolume
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0.5f); // stores the slider value to the player pref sfxVolume
    }

    void Update()
    {
        music.volume = musicSlider.value;//updates the music volume to the slider value
        sfx.volume = sfxSlider.value;//updates the sfx volume to the slider value
    }

    public void SetVolumePrefs()
    {
        PlayerPrefs.SetFloat("musicVolume", music.volume); //sets the playerpref so it can be accessed in the other scenes
        PlayerPrefs.SetFloat("sfxVolume", sfx.volume);//same as above
    }

}
