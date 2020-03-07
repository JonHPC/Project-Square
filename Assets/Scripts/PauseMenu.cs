using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public PlayerController player;

    public void Pause()
    {
        //stops time
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        //resumes the game
        Time.timeScale = 1f;

    }

    public void MainMenu()
    {
        //returns to main menu
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        //quits the application
        Application.Quit();
        Debug.Log("Pause Menu Quit");
    }
}
