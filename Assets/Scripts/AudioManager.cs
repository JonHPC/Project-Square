using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]//creates a slider in the inspector
    public float volume = 0.5f;

    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
    }

}

public class AudioManager : MonoBehaviour
{

    //create a singleton
    public static AudioManager instance;


    [SerializeField]
    Sound[] sounds;//an array of sounds

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one AudioManager in the scene.");
        }
        else
        {
            instance = this;
        }

    }
    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)//this for loop creates a gameobject for each sound
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);//sets the game obejcts as a child to the gameController gameobject
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
            _go.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("sfxVolume", 0.5f);
        }

        //PlaySound("GoodSquare");//test sound
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)//searches for the sound you want
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();//plays the sound you want
                return;
            }
        }

        //no sound with _name
        Debug.LogWarning("AudioMananger: Sound not found in list: " + _name);
    }

    void Update()
    {
        
    }

}
