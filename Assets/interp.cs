using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interp : MonoBehaviour
{
    //causing light to interpolate
    float duration = 1.0f;

    Color color0 = Color.white;
    Color color1 = Color.blue;
    Color color2 = Color.green;

    Light lt;

    //private float xPos, yPos;

    // Start is called before the first frame update
    void Start()
    {
        lt = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color3 = color1 * color2;
        float t = Mathf.PingPong(Time.time, duration) / duration;
        lt.color = Color.Lerp(color0, color3, t);
        


    }
}
