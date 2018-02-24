﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour {

    public GameObject shot;
    public float shootInterval;
    public float shotSpeed;
    
    private bool isSettled = false;
    private bool spacePressed;
    private float firstValue, nextValue;
    

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isSettled)
            Shoot();
    }

    void Shoot()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            spacePressed = true;
            if (spacePressed)
            {
                if (Time.time >= nextValue)
                {
                    GameObject instshot = Instantiate(shot, transform.position + transform.up * 3.5f, Quaternion.LookRotation(transform.up));
                    instshot.transform.up = transform.up;
                    Rigidbody rb = instshot.GetComponent<Rigidbody>();
                    rb.velocity = transform.up * shotSpeed;
                    nextValue += shootInterval;
                }
            }
        }
        else if (Input.GetKey(KeyCode.Space) != true)
        {
            spacePressed = false;
        }
    }

    public void TurretIsInitialized(string message)
    {
        if (message.Equals("TurretInitialized"))
        {
            isSettled = true;
        }
    }
}
