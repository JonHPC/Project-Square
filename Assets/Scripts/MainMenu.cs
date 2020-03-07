using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource menuSFX;

    public void StartGame()
    {
        //Starts the game when pressed
        SceneManager.LoadScene(1);//loads the Main scene
        Debug.Log("Start Game");
    }

    public void Quit()
    {
        Application.Quit();//quits the game
        Debug.Log("Quit");
    }

    public void Logo()
    {
        Application.OpenURL("https://teabunnystudios.com");
        Debug.Log("Logo");
    }

    public void menuSound()
    {
        menuSFX.Play();
    }
}
