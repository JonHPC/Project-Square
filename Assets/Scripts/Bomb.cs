using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float moveSpeed = 2f;//initial move speed


    //public GameController gameController;//references the GameController script

  

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 1 * Time.deltaTime * moveSpeed, 0);
    }

   

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Border")//if this object collides with the border....
        {
            Destroy(gameObject);// destroy this object
        }
    }
}
