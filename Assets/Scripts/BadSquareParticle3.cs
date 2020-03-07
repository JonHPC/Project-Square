using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadSquareParticle3 : MonoBehaviour
{
    public float timeLeft;

    void Start()
    {
        gameObject.transform.parent = null;
        gameObject.GetComponent<ParticleSystem>().Play();//plays particle animation on spawn
    }

    /*void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }*/
}
