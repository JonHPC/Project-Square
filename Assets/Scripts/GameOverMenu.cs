using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    private float finalScore;
    public TextMeshProUGUI finalScoreText;

    private float highScore;

    public GameObject newHighScore;

    void Start()
    {

        finalScore = PlayerPrefs.GetFloat("score", 0f);//stores the score player pref into this variable
        finalScoreText.text = "Final Score: " + finalScore;//sets the final score text with the final score player pref

        highScore = PlayerPrefs.GetFloat("highScore", 0f);//gets and stores the highScore player pref into this variable


        if(finalScore >= highScore)
        {
            newHighScore.SetActive(true);//new high score appears when you have a new high score
        }
        else
        {
            newHighScore.SetActive(false);//this notice is set invisible by default
        }

    }

    public void Restart()
    {
        //Get current scene name
        string scene = SceneManager.GetActiveScene().name;
        //Load it
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);//loads the Title scene
        Debug.Log("Main Menu");
    }

    public void Quit()
    {
        Application.Quit();//quits the app
        Debug.Log("Quit");
    }
}
