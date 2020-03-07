using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float distance = 20f;//distance from camera
    public float dragSpeed = 10f;//how fast the player moves
    public int chain;//stores the chain
    public Material[] colorChain;//tracks the current chain and changes the material of the player
    public int colorChainTracker;//tracks the current chain to match the player's material to
    public int color;//tracks the current color guage amount
    public int colorAdded; //tracks the amount of color added with the chain

    public bool isDead = false;//checks if player is dead
    public bool wrongColor;//used for game over menu to show wrong color message


    public GameObject floatingText;//used to show floating text on collectibles

    public GameController gameController;//references the GameController script

    public CameraShake cameraShake;//references the CameraShake script

    public AudioSource goodSound;//references the audio source
    public AudioSource badSound;//references the audio source

    public AudioManager audioManager;

    void Start()
    {
        isDead = false;//initializes this
        color = 50;//initializes the color to the middle of the gauge
        colorAdded = 3;//default color added
        wrongColor = false;//initialies this reason of death to false
        goodSound.pitch = 0.5f;
        audioManager = AudioManager.instance;
        colorChainTracker = 0; //initializes the colorChainTracker to 0
        this.GetComponent<MeshRenderer>().material = colorChain[colorChainTracker];//resets the player material to default

    }

    // Update is called once per frame
    void Update()
    {
        foreach(Touch touch in Input.touches)
        {
            if(EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                //you touched at least one UI element
                return;
            }
        }

        //you didnt touch any UI element, do this code
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

            colorAdded += 2; //adds 2 to the color added per chain
            colorAdded = Mathf.Clamp(colorAdded, 3, 20);//clamps the color added between 3 and 20
            color += colorAdded;//adds the new total to the color

            goodSound.pitch += 0.1f;
            goodSound.pitch = Mathf.Clamp(goodSound.pitch, 0.5f, 1.5f);//clamps the pitch between 0.5 and 1.5f

            colorChainTracker += 1; //adds one to the this which makes the player more green/intense
            colorChainTracker = Mathf.Clamp(colorChainTracker, 0, 9);//clamps the value to a maximum of 5
            this.GetComponent<MeshRenderer>().material = colorChain[colorChainTracker];//sets the player material to match the current intensity of the chain bonus


            /*if(chain < 3)
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
            }*/



            if(floatingText)//shows floating text
            {
                //GameObject floatingTextSpawn = Instantiate(floatingText, other.transform.position, Quaternion.identity);//spawns the floating text prefab at the location of the player, doesnt follow the player
                //floatingTextSpawn.GetComponent<TextMeshPro>().text = colorAdded.ToString();//sets the text to show the amount of color added
                ShowFloatingText();//runs this function if floatingText prefab exists
            }

            Destroy(other.gameObject);//destroy the other game object
            goodSound.PlayOneShot(goodSound.clip, 0.5f);
            //audioManager.PlaySound("GoodSquare");


        }

        if (other.gameObject.tag == "BadSquare")
        {
            color = color - 20;//subtracts from the color gauge
            chain = 0;//resets the chain
            goodSound.pitch = 0.5f;//resets the pitch
            colorAdded = 3;//resets the color added
            PlayerPrefs.SetInt("chain", chain);//updates the player pref for chain
            colorChainTracker = 0;//resets the chain
            this.GetComponent<MeshRenderer>().material = colorChain[colorChainTracker]; //resets the player's material to default upon hitting a red square


            if(color > 0)
            {
                StartCoroutine(cameraShake.Shake(0.15f, 0.4f));

                Destroy(other.gameObject);//destroys the bad square
                badSound.Play();
                audioManager.PlaySound("BadSquare");
            }
            else
            {
                StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
                Destroy(other.gameObject);
                badSound.Play();
                audioManager.PlaySound("BadSquare");
                isDead = true;//sets player to be dead
                wrongColor = true;//reason of death is wrong color, used for game over menu

            }


        }

        if(other.gameObject.tag == "Bomb")
        {
            GameObject[] badSquares; //creates an array of bad squares

            badSquares = GameObject.FindGameObjectsWithTag("BadSquare");//finds all badsquares and adds them to this array

            foreach(GameObject badSquare in badSquares)//goes thru the array
            {
                Destroy(badSquare);//and destroys each of these bad squares
                 
            }

            StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
            audioManager.PlaySound("Bomb");
            Destroy(other.gameObject);//destroys the bomb
        }

        if(other.gameObject.tag == "RandomSquare")
        {
            int effect = Random.Range(1, 3); //upon striking a random square, roll a random number and give the player an effect based on the roll
            if (effect == 1){
                Debug.Log("Shield On");
            }
            else if(effect == 2){
                Debug.Log("Pulse Waves On");
            }
            else{
                Debug.Log("All square are bad On");
            }

            Destroy(other.gameObject);//destroys the random square
        }
    }

    void ShowFloatingText()
    {
        GameObject floatingTextSpawn = Instantiate(floatingText, transform.position, Quaternion.identity);//spawns the floating text prefab at the location of the player, doesnt follow the player
        floatingTextSpawn.GetComponent<TextMeshPro>().text = colorAdded.ToString();//sets the text to show the amount of color added

    }
}
