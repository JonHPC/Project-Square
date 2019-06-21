using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float distance = 20f;//distance from camera
    public float dragSpeed = 10f;//how fast the player moves
    public int chain;//stores the chain
    public int color;//tracks the current color guage amount
    public int colorAdded; //tracks the amount of color added with the chain

    public bool isDead = false;//checks if player is dead
    public bool wrongColor;//used for game over menu to show wrong color message

    public GameObject floatingText;//used to show floating text on collectibles

    public GameController gameController;//references the GameController script

    public CameraShake cameraShake;//references the CameraShake script

    public AudioSource goodSound;//references the audio source
    public AudioSource badSound;//references the audio source

    void Start()
    {
        isDead = false;//initializes this
        color = 50;//initializes the color to the middle of the gauge
        wrongColor = false;//initialies this reason of death to false
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && isDead==false)
        {
            Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                // get the touch position from the screen touch to world point
                Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, distance));
                // lerp and set the position of the current object to that of the touch, but smoothly over time.
                transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime * dragSpeed);
                //transform.position = Vector3.MoveTowards(transform.position, touchedPos, Time.deltaTime * dragSpeed);
            }
        }




    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GoodSquare")//if this object collides with the Player
        {
            chain += 1;//adds one to the chain
            PlayerPrefs.SetInt("chain", chain);//updates the playerpref for the current chain


            if(chain < 3)
            {
                colorAdded = 5;
                color += colorAdded;//adds 5 to color gauge when under a 3 chain combo

            }
            else if(chain >= 3 && chain < 5)
            {
                colorAdded = 10;
                color += colorAdded;//adds 10 to color gauge for chain 3 and 4 
                gameController.chainAnimPlaying = true;//triggers the chain animation once
            }
            else if(chain >= 5 && chain < 10)
            {
                colorAdded = 15;
                color += colorAdded;//adds 15 to color gauge for chain 5 and up
                gameController.biggerChainAnimPlaying = true;//triggers the bigger chain animation once
            }
            else if(chain >= 10)
            {
                colorAdded = 20;
                color += colorAdded;
                gameController.biggerChainAnimPlaying = true;
            }

            if(floatingText)//shows floating text
            {
                //GameObject floatingTextSpawn = Instantiate(floatingText, other.transform.position, Quaternion.identity);//spawns the floating text prefab at the location of the player, doesnt follow the player
                //floatingTextSpawn.GetComponent<TextMeshPro>().text = colorAdded.ToString();//sets the text to show the amount of color added
                ShowFloatingText();//runs this function if floatingText prefab exists
            }

            Destroy(other.gameObject);//destroy the other game object
            goodSound.Play();

        }

        if (other.gameObject.tag == "BadSquare")
        {
            color = color - 20;//subtracts from the color gauge
            chain = 0;//resets the chain
            PlayerPrefs.SetInt("chain", chain);//updates the player pref for chain

            if(color > 0)
            {
                StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
                Destroy(other.gameObject);//destroys the bad square
                badSound.Play();
            }
            else
            {
                StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
                Destroy(other.gameObject);
                badSound.Play();
                isDead = true;//sets player to be dead
                wrongColor = true;//reason of death is wrong color, used for game over menu

            }


        }
    }

    void ShowFloatingText()
    {
        GameObject floatingTextSpawn = Instantiate(floatingText, transform.position, Quaternion.identity);//spawns the floating text prefab at the location of the player, doesnt follow the player
        floatingTextSpawn.GetComponent<TextMeshPro>().text = colorAdded.ToString();//sets the text to show the amount of color added

    }
}
