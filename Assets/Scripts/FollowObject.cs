// A simple script that moves the camera around

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{

	public Transform target;
	public bool lookAtTarget = true;
	private Vector3 offset;
	private float originalY;
	//private Vector3 prevTargetPosition;
	//private Vector3 targetVelocity;

	void Start()
	{
		offset = transform.position - target.transform.position;
		//prevTargetPosition = target.transform.position;
		//targetVelocity = new Vector3(0, 0, 0);
		originalY = transform.position.y;
	}

    // Update is called once per frame
    void Update()
    {
        //targetVelocity = prevTargetPosition - target.transform.position;

        transform.position = new Vector3(target.transform.position.x + offset.x , originalY, target.transform.position.z + offset.z);
        if(lookAtTarget) transform.LookAt(target);

        //prevTargetPosition = target.transform.position;
    }
}
