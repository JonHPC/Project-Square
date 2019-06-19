using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodSquare : MonoBehaviour
{
    public float moveSpeed = 2f;//initial move speed
    public float scoreMultiplier = 1f;//initial score multiplier
    public float baseScore = 100;//base score value
    public float score;//calculated score

    //public GameController gameController;//references the GameController script

    void Start()
    {
        score = baseScore * scoreMultiplier;//initializes the final score based on the scale of the square

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y-1*Time.deltaTime*moveSpeed, 0);
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")//if this object collides with the Player
        {
            gameController.score += score;//adds the score value of this square to the total score
            gameController.UpdateScore();


            Destroy(gameObject);//destroy this game object

        }
    }*/

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Border")//if this object collides with the border....
        {
            Destroy(gameObject);// destroy this object
        }
    }
}
