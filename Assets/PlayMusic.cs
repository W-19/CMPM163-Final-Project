using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public Transform target;
    float distanceToTarget;


    void Start()
    {
        GetComponent<AudioSource>().volume = 0;
        // StartCoroutine(AdjustVolume());

    }
    void Update()
    {
        
        distanceToTarget = Vector3.Distance(transform.position, target.position);
        //Debug.Log(distanceToTarget);
        if (distanceToTarget < 16.0f) 
        {
           // GetComponent<AudioSource>.Play();

            //if (GetComponent<AudioSource>().isPlaying)
            // { // do this only if some audio is being played in this gameObject's AudioSource

             // Assuming that the target is the player or the audio listener

                //if (distanceToTarget < 1) 

                GetComponent<AudioSource>().volume = 1 - (distanceToTarget/16);

                //yield return new WaitForSeconds(1); // this will adjust the volume based on distance every 1 second (Obviously, You can reduce this to a lower value if you want more updates per second)

            //}
        }
        else
        {
            GetComponent<AudioSource>().volume = 0;
        }
    }

   /* IEnumerator AdjustVolume()
    {

     while (true)
                {
           if (distanceToTarget < 1) { GetComponent<AudioSource>().Play; }
            {
                if (GetComponent<AudioSource>().isPlaying)
                { // do this only if some audio is being played in this gameObject's AudioSource

                    float distanceToTarget = Vector3.Distance(transform.position, target.position); // Assuming that the target is the player or the audio listener

                    if (distanceToTarget < 1) { distanceToTarget = 1; }

                    GetComponent<AudioSource>().volume = 6 / distanceToTarget;

                    yield return new WaitForSeconds(1); // this will adjust the volume based on distance every 1 second (Obviously, You can reduce this to a lower value if you want more updates per second)

                }
            //}
                 }

    }*/
}