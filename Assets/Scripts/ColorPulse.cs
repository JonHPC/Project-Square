using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPulse : MonoBehaviour
{
    public float rate = 1f;//rate of color change

    private MeshRenderer meshRender; //will store the material renderer
    private Color colorStart;
    private Color colorEnd;
    private float i;


    // Start is called before the first frame update
    void Start()
    {
        colorEnd = new Color(Random.value, Random.value, Random.value);
        colorStart = colorEnd;
        //colorEnd = new Color(45, 243, 79);
        //colorStart = new Color(4, 15, 6);
        colorEnd.a = 0;
        meshRender = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Blend towards the current target color
        i += Time.deltaTime * rate;
        meshRender.material.color = Color.Lerp(colorStart, colorEnd, Mathf.PingPong(i * 2, 1));

        //If we get to the target color, choose a new target color
        if(i >= 1) {
            i = 0;
            colorEnd = new Color(Random.value, Random.value, Random.value);
            //colorEnd = new Color(45, 243, 79);
            colorStart = colorEnd;
            //colorStart = new Color(4, 15, 6);
            colorEnd.a = 0;
        }

    }
}
