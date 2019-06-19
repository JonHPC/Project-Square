using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public PlayerController playerController;//accesses the PlayerController script
    public GameObject player;//references the player game object

    public GameObject goodSquare;//references the downward falling good square
    public GameObject goodSquare2;//references the leftward good square
    public GameObject goodSquare3;//references the upward good square
    public GameObject goodSquare4;//references the rightward  good square

    public GameObject badSquare;//references the downward falling bad square
    public GameObject badSquare2;//references the leftward bad square
    public GameObject badSquare3;//references the upward bad square
    public GameObject badSquare4;//references the rightward bad square

    public float xSpawnMin = -6.5f;//the furthest left spawn coordinate
    public float xSpawnMax = 6.5f;//the furthest right spawn coordinate
    public float ySpawnMin = -13f;//the furthest down spawn coordinate
    public float ySpawnMax = 13f;// the furthest up spawn coordinate

    public float squareSpeed = 2f;//initial square speed
    public float spawnTimerAmount = 1f;//timer amount
    public float spawnTimer;//countdown spawn timer

    public int level = 1;//inital level
    public float levelTimer = 0f;//initial level timer
    public TextMeshProUGUI levelText;//displays the current level

    public TextMeshProUGUI scoreText;//displays the current score
    public float score;//stores the score

    public TextMeshProUGUI highScoreText;// displays the current high score
    public float highScore;//stores the high score

    public GameObject gameOverMenu;
    





    // Start is called before the first frame update
    void Start()
    {
        score = 0f;//sets the initial score to 0
        level = 1;//sets the level back to 1
        Time.timeScale = 1f;//sets time to normal
        gameOverMenu.gameObject.SetActive(false);//turns off the game over menu
        player.gameObject.SetActive(true);//makes sure the player game object is set active
        highScore = PlayerPrefs.GetFloat("highScore", 0f);//initializes the high score
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpeed();//updates the speed of the squares
        SpawnSquare();//runs the SpawnSquare function
        UpdateLevel();//runs the UpdateLevel function
        UpdateScore();//runs the UpdateScore function
        CheckIfDead();//checks if the player is dead
        //squareSpeed = Random.Range(2f, 8f);
        //spawnTimerAmount = Random.Range(0.1f, 0.5f);

    }

    void SpawnSquare()
    {
        spawnTimer -= Time.deltaTime;//starts the countdown timer

        int isGood = Random.Range(1, 3);//1 is good, 2 is bad



        int side = Random.Range(1, 5);//1 is top, 2 is right, 3 is bottom, 4 is left spawns
        float scale = Random.Range(0.3f, 1.5f);//randomly scales the spawned scale from 0.3 to 1.5

        float x = Random.Range(xSpawnMin, xSpawnMax);//gets a random coordinate between the min x and max x values
        float y = Random.Range(ySpawnMin, ySpawnMax);//gets a random coordinate between the min y and max y values

        if(spawnTimer <= 0f)//once the timer hits 0...
        {
            if(isGood == 1)//spawns good squares
                {
                    switch (side)
                    {
                        case 1:
                            GameObject goodSquareClone = Instantiate(goodSquare, new Vector3(x, ySpawnMax, 0f), Quaternion.identity);//spawn a good square falling down
                            goodSquareClone.transform.localScale = new Vector3(scale, scale, scale);//scales this square to a random size
                            //goodSquareClone.GetComponent<GoodSquare>().scoreMultiplier = 1 / scale;//changes the score multiplier to make smaller squares worth more
                            spawnTimer = spawnTimerAmount;//resets the spawn timer
                            break;
                        case 2:
                            GameObject goodSquare2Clone = Instantiate(goodSquare2, new Vector3(xSpawnMax, y, 0f), Quaternion.identity);//spawn a good square going left
                            goodSquare2Clone.transform.localScale = new Vector3(scale, scale, scale);
                            spawnTimer = spawnTimerAmount;//resets the spawn timer
                            break;
                        case 3:
                            GameObject goodSquare3Clone = Instantiate(goodSquare3, new Vector3(x, ySpawnMin, 0f), Quaternion.identity);//spawn a good square going upwards
                            goodSquare3Clone.transform.localScale = new Vector3(scale, scale, scale);
                            spawnTimer = spawnTimerAmount;//resets the spawn timer
                            break;
                        case 4:
                            GameObject goodSquare4Clone = Instantiate(goodSquare4, new Vector3(xSpawnMin, y, 0f), Quaternion.identity);//spawn a good square going right
                            goodSquare4Clone.transform.localScale = new Vector3(scale, scale, scale);
                            spawnTimer = spawnTimerAmount;//resets the spawn timer
                            break;
                    }
                }

            else if (isGood == 2)//spawns bad squares
                {
                    switch (side)
                    {
                        case 1:
                            GameObject badSquareClone = Instantiate(badSquare, new Vector3(x, ySpawnMax, 0f), Quaternion.identity);//spawn a bad square falling down
                            badSquareClone.transform.localScale = new Vector3(scale, scale, scale);
                            spawnTimer = spawnTimerAmount;//resets the spawn timer
                            break;
                        case 2:
                            GameObject badSquare2Clone = Instantiate(badSquare2, new Vector3(xSpawnMax, y, 0f), Quaternion.identity);//spawn a bad square going left
                            badSquare2Clone.transform.localScale = new Vector3(scale, scale, scale);
                            spawnTimer = spawnTimerAmount;//resets the spawn timer
                            break;
                        case 3:
                            GameObject badSquare3Clone = Instantiate(badSquare3, new Vector3(x, ySpawnMin, 0f), Quaternion.identity);//spawn a bad square going upwards
                            badSquare3Clone.transform.localScale = new Vector3(scale, scale, scale);
                            spawnTimer = spawnTimerAmount;//resets the spawn timer
                            break;
                        case 4:
                            GameObject badSquare4Clone = Instantiate(badSquare4, new Vector3(xSpawnMin, y, 0f), Quaternion.identity);//spawn a bad square going right
                            badSquare4Clone.transform.localScale = new Vector3(scale, scale, scale);
                            spawnTimer = spawnTimerAmount;//resets the spawn timer
                            break;
                    }
                }

        }
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;//updates the score text
        PlayerPrefs.SetFloat("score", score);//updates the score player pref

        if(score > highScore)//only updates the high score if the score if higher
        {
            highScore = score;
            PlayerPrefs.SetFloat("highScore", highScore);//updates the PlayerPrefs for highScore

        }

        highScoreText.text = "High Score: " + highScore;//updates the high score text
    }

    public void UpdateSpeed()
    {
        goodSquare.GetComponent<GoodSquare>().moveSpeed = squareSpeed;//updates the GoodSquare to match the current square speed
        goodSquare2.GetComponent<GoodSquare2>().moveSpeed = squareSpeed;//updates the GoodSquare2 to match the current square speed
        goodSquare3.GetComponent<GoodSquare3>().moveSpeed = squareSpeed;//updates the GoodSquare3 to match the current square speed
        goodSquare4.GetComponent<GoodSquare4>().moveSpeed = squareSpeed;//updates the GoodSquare4 to match the current square speed

        badSquare.GetComponent<BadSquare>().moveSpeed = squareSpeed;//updates the BadSquare to match the current square speed
        badSquare2.GetComponent<BadSquare2>().moveSpeed = squareSpeed;//updates the BadSquare to match the current square speed
        badSquare3.GetComponent<BadSquare3>().moveSpeed = squareSpeed;//updates the BadSquare to match the current square speed
        badSquare4.GetComponent<BadSquare4>().moveSpeed = squareSpeed;//updates the BadSquare to match the current square speed
    }

    public void UpdateLevel()
    {
        levelTimer += Time.deltaTime;//starts the level timer

        levelText.text = "Level: " + level;//updates the level text

        if(levelTimer >= 5f)//after x seconds pass, the level increases by 1
        {
            levelTimer = 0f;//resets the level timer to 0
            level += 1;//adds 1 to the level

            if(level <= 5)
            {
                squareSpeed += 0.4f;
                spawnTimerAmount -= 0.1f;


            }
            else if (level >=6)
            {
                squareSpeed += 0.4f;
                squareSpeed = Mathf.Clamp(squareSpeed, 2f, 8f);
                
                spawnTimerAmount -= 0.03f;
                spawnTimerAmount = Mathf.Clamp(spawnTimerAmount, 0.1f, 1f);
            }
            else if(level >= 20)
            {
                squareSpeed += 0.4f;
                squareSpeed = Mathf.Clamp(squareSpeed, 2f, 8f);
                spawnTimerAmount -= 0.03f;
                spawnTimerAmount = Mathf.Clamp(spawnTimerAmount, 0.1f, 1f);
            }
        }
    }

    public void CheckIfDead()
    {
        bool isDead = playerController.isDead;

        if(isDead)
        {
            gameOverMenu.gameObject.SetActive(true);//makes the game over menu visible
            StartCoroutine(delayDeath());//runs the delayDeath coroutine
        }
    }

    IEnumerator delayDeath()
    {
        yield return new WaitForSeconds(1);//delays deactivating the player game object for x seconds
        player.gameObject.SetActive(false);//if player is dead, deactivate it after
    }
}


//x -7 to 7
//y 13 to -13
//player gets longer each good square he eats. Getting hit with bad square shortens. If you get hit when you are one unit size, u lose. Objective is to get to a certain length per wave?
//
//Might be more complex to code, but when the bad square could take off a whole chunk of the tail depending where it hits!
//this game is snakes and squares!
//the player snake changes color once it is long enough to clear a level