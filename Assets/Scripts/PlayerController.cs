using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float distance = 20f;//distance from camera
    public float dragSpeed = 10f;//how fast the player moves
    public float score;//stores the score
    public int hp = 3;//the player's hp

    public TextMeshProUGUI hpText;

    public bool isDead = false;//checks if player is dead

    public GameController gameController;//references the GameController script

    public CameraShake cameraShake;//references the CameraShake script

    public AudioSource goodSound;//references the audio source
    public AudioSource badSound;//references the audio source

    void Start()
    {
        isDead = false;//initializes this
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

        hpText.text = "HP: " + hp;//updates the hp text
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GoodSquare")//if this object collides with the Player
        {
            score = score + 1;
            gameController.score = score;//adds the score value of this square to the total score
            Destroy(other.gameObject);//destroy the other game object
            goodSound.Play();

        }

        if (other.gameObject.tag == "BadSquare")
        {
            //score = score - 1;
            //gameController.score = score;
            hp -= 1;//subtracts 1 hp

            if (hp > 0)
            {
                StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
                Destroy(other.gameObject);
                badSound.Play();
            }
            else
            {
                StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
                Destroy(other.gameObject);
                badSound.Play();
                isDead = true;//sets player to be dead
                //Time.timeScale = 0f;
                //this.gameObject.SetActive(false);//once hp hits 0, player is set inactive
            }
        }
    }
}
