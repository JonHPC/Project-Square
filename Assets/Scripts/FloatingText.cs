using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{

    public float destroyTime = 1f;//the time it takes to destroy this object
    public Vector3 offset = new Vector3(0, 1.5f, 0);//offsets the text by a bit from the player

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);//destroys the text after this many seconds

        transform.localPosition += offset;//offsets the text when it spawns
    }


}
