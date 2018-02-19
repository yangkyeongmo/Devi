﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvisorMovement : MonoBehaviour {

    public float speed;
    public float approachSpeed;
    public float approachRange;
    public float arriveRange;

    private bool isArrived = false;
    private CreateBuildings cb;
    private float targetMidpointNum;
    private GameObject player;
    private GameObject targetMidpoint;
    private Vector3 moveDirection;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindWithTag("Player");
        cb = player.GetComponent<CreateBuildings>();

        //Find unoccupied zone and set as target
        int[] zoneOccupiedNum = cb.GetOccupyInfo();
        int randomNum = Random.Range(0, zoneOccupiedNum.Length);
        while(zoneOccupiedNum[randomNum] == 1)
        {
            randomNum = Random.Range(0, zoneOccupiedNum.Length);
            if(zoneOccupiedNum[randomNum] != 1)
            {
                break;
            }
        }
        targetMidpoint = GameObject.Find("MidPoint" + randomNum);
        Debug.Log("Target Midpoint #: " + randomNum);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //move to player itself
        if ((transform.position - player.transform.position).magnitude >= approachRange)
        {
            moveDirection = (player.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed;
            transform.up = moveDirection;                                                                                           //Up direction parralell to move direction
        }
            
        //move to target point from certain range
        if((transform.position - player.transform.position).magnitude <= approachRange)
        {
            moveDirection = (player.transform.position - transform.position).normalized;
            transform.position += moveDirection * approachSpeed;
            transform.up = Quaternion.FromToRotation(transform.up, -moveDirection) * transform.up * Time.deltaTime;
            //transform.up = -moveDirection;                                                                                          //Up direction opposite to move direction 
        }


        if ((transform.position - player.transform.position).magnitude <= arriveRange)
        {
            isArrived = true;
        }
    }

    public bool GetIsArrived()
    {
        return isArrived;
    }

}
