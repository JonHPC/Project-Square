using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBGController : MonoBehaviour
{
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

    public float squareSpeed = 4f;//initial square speed
    public float spawnTimerAmount = 0.3f;//timer amount
    public float spawnTimer;//countdown spawn timer

    void Start()
    {

        Time.timeScale = 1f;//sets time to normal
    }

    void Update()
    {
        SpawnSquare();//starts spawning squares
        UpdateSpeed();
    }

    void SpawnSquare()
    {
        spawnTimer -= Time.deltaTime;//starts the countdown timer

        int isGood = Random.Range(1, 3);//1 is good, 2 is bad



        int side = Random.Range(1, 5);//1 is top, 2 is right, 3 is bottom, 4 is left spawns
        float scale = Random.Range(0.3f, 1.5f);//randomly scales the spawned scale from 0.3 to 1.5

        float x = Random.Range(xSpawnMin, xSpawnMax);//gets a random coordinate between the min x and max x values
        float y = Random.Range(ySpawnMin, ySpawnMax);//gets a random coordinate between the min y and max y values

        if (spawnTimer <= 0f)//once the timer hits 0...
        {
            if (isGood == 1)//spawns good squares
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
}
