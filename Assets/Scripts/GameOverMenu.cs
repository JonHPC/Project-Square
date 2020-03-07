using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public PlayerController playerController;
    public GameController gameController;

    private int finalLevel;//tracks the final level reached
    private int maxChain;//tracks the max chain reached this round

    public TextMeshProUGUI finalLevelText;//shows the final level reached that game session
    public TextMeshProUGUI deathText;//shows the reason for losing
    public TextMeshProUGUI maxChainText;//shows the max chain achieved this round
    private int highLevel;//stores the high level

    public GameObject newHighLevel;//the new high level message
    

    void Start()
    {

        finalLevel = PlayerPrefs.GetInt("level", 1);//stores the level player pref into this variable
        finalLevelText.text = "Level Reached: " + finalLevel;//sets the level reached player pref

        highLevel = PlayerPrefs.GetInt("highLevel", 1);//gets and stores the highLevel player pref into this variable


       if(finalLevel >= highLevel)
        {
            newHighLevel.SetActive(true);//new high level appears when you have a new high level
        }
        else
        {
            newHighLevel.SetActive(false);//this notice is set invisible by default
        }

        maxChain = PlayerPrefs.GetInt("maxChain", 0);//pulls the player pref for max chain
        maxChainText.text = "Max Chain: " + maxChain;//displays the max chain reached this round

        if(playerController.wrongColor == true)
        {
            deathText.text = "YOU LOSE";//shows this message if you die due to wrong color
        }
        else if(gameController.timeOut == true)
        {
            deathText.text = "OUT OF TIME";//shows this message if you run out of time
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
