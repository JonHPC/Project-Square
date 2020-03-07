using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

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

    public GameObject randomSquare;//references the downward falling random square

    public GameObject bomb;//references the bomb prefab
    public float bombSpawn = 10f; //every 10 seconds

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
    public TextMeshProUGUI levelTimerText;//displays the level timer
    public Slider levelTimerGauge;//displays the level timer gauge
    public GameObject timerColor;//changes the timer color gauge
    public bool timeOut;//used for game ove rmenu to show cause of death


    public Slider colorGauge;//displays the current color gauge

    public int highLevel;//stores the highest level achieved
    public TextMeshProUGUI highLevelText;


    public GameObject levelUpText;//shows the level up text

    public TextMeshProUGUI chainText;//displays the current chain
    public int chain = 0;//stores the chain
    public bool chainAnimPlaying = false;//accessed by playercontroller to trigger chain anim
    public bool biggerChainAnimPlaying = false;

    public TextMeshProUGUI maxChainText;// displays the max chain ever
    public int maxChain = 0;//stores the max chain
    public int highestChainEver;//stores the global highest chain for e peen

    public GameObject gameOverMenu;

    //cache
    private AudioManager audioManager;




    // Start is called before the first frame update
    void Start()
    {
        //score = 0f;//sets the initial score to 0
        level = 1;//sets the level back to 1
        PlayerPrefs.SetInt("level",level);//sets the player pref for level
        PlayerPrefs.SetInt("chain", chain);//sets the current chain
        PlayerPrefs.SetInt("maxChain", 0);//resets the max chain for the round
        levelTimer = 30f;//initializes the timer to 30 seconds
        Time.timeScale = 1f;//sets time to normal
        timeOut = false;//used for game over menu
        gameOverMenu.gameObject.SetActive(false);//turns off the game over menu
        player.gameObject.SetActive(true);//makes sure the player game object is set active
        highLevel = PlayerPrefs.GetInt("highLevel", 1);//initializes the high level, default is level 1
        levelUpText.SetActive(false);//initializes this to false
        maxChain = PlayerPrefs.GetInt("maxChain", 0);//initializes the max chain, default is 0
        highestChainEver = PlayerPrefs.GetInt("highestChainEver", 0);//initializes the highest chain

        //caching
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("No AudioManager found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpeed();//updates the speed of the squares
        SpawnSquare();//runs the SpawnSquare function
        //SpawnExtras();//runs the SpawnExtras
        UpdateLevel();//runs the UpdateLevel function
        UpdateChain();//runs the UpdateChain function
        UpdateColorGauge();//updates the color gauge slider
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

    /*public void SpawnExtras()
    {

        bombSpawn -= Time.deltaTime;//while the timer counts down

        if(bombSpawn <= 0f)//when less than 10 seconds
        {
            float bombChance = Random.Range(1, 10);//roll the dice
            if(bombChance < 2)//a 20% to spawn a bomb
            {
                float x = Random.Range(xSpawnMin, xSpawnMax);//gets a random coordinate between the min x and max x values
                Instantiate(bomb, new Vector3(x, ySpawnMax, 0f), Quaternion.identity);//spawns a bomb falling down
                bombSpawn = 10f;//resets the bomb spawn timer
            }
        }
    }*/

    public void UpdateChain()
    {

        chain = PlayerPrefs.GetInt("chain", 0);//pulls the current chain amount from player prefs
        if(chain < 3)
        {
            chainText.text = "";//disables text before 3 chain
        }
        else
        {
            chainText.text = "Chain: " + chain;//updates the chain text with the current chain


            if(chainAnimPlaying)
            {
                chainText.gameObject.GetComponent<Animator>().SetTrigger("chainUp");//plays the chain up animation
                chainAnimPlaying = false;//prevents this trigger from firing more than once
            }
            else if(biggerChainAnimPlaying)
            {
                chainText.gameObject.GetComponent<Animator>().SetTrigger("biggerChainUp");//plays the bigger chain up animation
                biggerChainAnimPlaying = false;//prevents this trigger from firing more than once
            }

        }


        if (chain > maxChain)//only updates the max chain if chain is higher
        {
            maxChain = chain;
            PlayerPrefs.SetInt("maxChain", maxChain);//updates the PlayerPrefs for highScore

        }

        if(maxChain> highestChainEver)
        {
            highestChainEver = maxChain;//sets the highest chain to the current max
            PlayerPrefs.SetInt("highestChainEver", highestChainEver);//updates the player pref for this, will be displayed on title page with high level
        }

        maxChainText.text = "Max Chain: " + maxChain;//updates the maxChain text

    }

    public void UpdateColorGauge()
    {
        colorGauge.value = playerController.color;//updates the color gauge with the current player color value


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

        randomSquare.GetComponent<RandomSquare>().moveSpeed = squareSpeed;//updates the RandomSquare to match the current square speed
    }

    public void UpdateLevel()
    {
        if(playerController.isDead == false){
            levelTimer -= Time.deltaTime;//counts down the timer

            levelTimerText.text = "" + Mathf.Round(levelTimer);//displays the remaining time rounded down to the nearest second
            levelTimerGauge.value = levelTimer;//sets the level timer gauge to the level timer amount

            if(levelTimer <= 5)
            {
                //levelTimerText.color = new Color32(215, 40, 40, 255);//changes the timer text color to red for the last 5 seconds
                timerColor.GetComponent<Image>().color = new Color32(215, 40, 40, 255);//turns the color gauge to match above color

            }

            else if(levelTimer <= 15 && levelTimer > 5)
            {
                timerColor.GetComponent<Image>().color = new Color32(255, 255, 0, 255);//turns the color gauge to match above color

            }
            else
            {
                levelTimerText.color = new Color32(255, 255, 255, 255);//changes text to white in all other cases
                timerColor.GetComponent<Image>().color = new Color32(255, 255, 255, 255);//turns the color gauge to match above color

            }

            levelText.text = "Level: " + level;//displays the current level

            if(playerController.color >= 100)
            {
                levelUp();//runs the levelUp function if the player gets enough color
            }

            if(level > highLevel)
            {
                highLevel = PlayerPrefs.GetInt("highLevel", 1);//gets the current high level level player pref
            }

            highLevelText.text = "High Level: " + highLevel;//displays the current high level

        }

        if(levelTimer <= 0)
        {
            playerController.isDead = true;// if the time runs out, kill the player
            timeOut = true;//used for game over menu
        }



        /*levelTimer += Time.deltaTime;//starts the level timer

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
        }*/
    }

   



    public void levelUp()
    {
        level += 1;//adds one to the level
        PlayerPrefs.SetInt("level", level);

        StartCoroutine(levelUpTextFn(level));//starts this couroutine with the current level

        if(level > highLevel)
        {
            PlayerPrefs.SetInt("highLevel", level);//updates the high level if the current level surpases it
        }

        //DestroyAll();//runs this function to clear the entire screen of squares upon leveling up

        playerController.color = 50;//resets the player's color gauge to 50


        audioManager.PlaySound("LevelUp");//plays the levelUp sound thru the audio manager

        //float x = Random.Range(xSpawnMin, xSpawnMax);//gets a random coordinate between the min x and max x values
       //Instantiate(bomb, new Vector3(x, ySpawnMax, 0f), Quaternion.identity);//spawns a bomb falling down as a reward

        if (level <= 5)
        {
            squareSpeed += 0.4f;
            spawnTimerAmount -= 0.1f;
            //levelTimer = 25f;//resets the level timer to 30 seconds
            levelTimer += 10f;//adds 10 seconds to the timer
            levelTimer = Mathf.Clamp(levelTimer, 0f, 30f);//clamps the value of the timer to 30 at max

        }
        else if (level >= 6 && level < 20)
        {
            squareSpeed += 0.4f;
            squareSpeed = Mathf.Clamp(squareSpeed, 2f, 8f);

            spawnTimerAmount -= 0.03f;
            spawnTimerAmount = Mathf.Clamp(spawnTimerAmount, 0.1f, 1f);

            //levelTimer = 15f;
            levelTimer += 10f;//adds 10 seconds to the timer
            levelTimer = Mathf.Clamp(levelTimer, 0f, 30f);//clamps the value of the timer to 30 at max
        }
        else if (level >= 20)
        {
            squareSpeed += 0.4f;
            squareSpeed = Mathf.Clamp(squareSpeed, 2f, 8f);
            spawnTimerAmount -= 0.03f;
            spawnTimerAmount = Mathf.Clamp(spawnTimerAmount, 0.1f, 1f);
            //levelTimer = 10f;
            levelTimer += 10f;//adds 10 seconds to the timer
            levelTimer = Mathf.Clamp(levelTimer, 0f, 30f);//clamps the value of the timer to 30 at max                                                                                  
        }

    }

    IEnumerator levelUpTextFn(int level)
    {
        levelUpText.SetActive(true);//sets this text to be active
        levelUpText.GetComponent<TextMeshProUGUI>().text = "LEVEL " + level;//sets the text of this pop up to match the current level
        yield return new WaitForSeconds(0.5f);
        levelUpText.SetActive(false);//disables the text after the wait
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
//player has to race the timer to get enough of the target color
//if player gets hit by the wrong color too many times, game over!


// Add apple game center integration for achievements and leaderboards!! Iphone first, later we do ipad
//add ability to post score on FB and twitter
//Achievements can include: Get a Chain, a 5 chain, 10 chain, 20 chain, etc
//Achievement: Collecting total number of squares over time, hitting x number of bad squares over time, hitting # of bad squares in a row
// Get to certain level achievements (ex. max level)
