using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideClear : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Cube") {
            other.GetComponent<PingPong_CellularAutomata>().StartCoroutine("clearArea");
            other.GetComponent<AudioScript>().max = 0.54f; ;
        }
    }
}
