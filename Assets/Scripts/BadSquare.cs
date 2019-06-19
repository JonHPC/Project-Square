using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadSquare : MonoBehaviour
{
    public float moveSpeed = 2f;//initial move speed

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 1 * Time.deltaTime * moveSpeed, 0);
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")//if this object collides with the Player
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);//destroy this game object

        }
    }*/

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Border")//if this object collides with the border....
        {
            Destroy(gameObject);// destroy this object
        }
    }
}

