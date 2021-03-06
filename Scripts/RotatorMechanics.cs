﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorMechanics : MonoBehaviour {

    public float rotateSpeed;

    private Rigidbody rb;
    private Rigidbody rbP;
    private GameObject player;
    private Vector3 initialPosition;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        rbP = player.GetComponent<Rigidbody>();
        initialPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = rbP.velocity;
        transform.localPosition = initialPosition;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (rb.angularVelocity.y < 10.0f)
            {
                rb.angularVelocity += new Vector3(0, rotateSpeed, 0);
                rbP.angularVelocity += new Vector3(0, rotateSpeed, 0) * 0.1f;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (rb.angularVelocity.y > -10.0f)
            {
                rb.angularVelocity += new Vector3(0, -rotateSpeed, 0);
                rbP.angularVelocity += new Vector3(0, -rotateSpeed, 0) * 0.1f;
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (rb.angularVelocity.x < 10.0f)
            {
                rb.angularVelocity += new Vector3(0, rotateSpeed, 0);
                rbP.angularVelocity += new Vector3(rotateSpeed, 0, 0) * 0.1f;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (rb.angularVelocity.x > -10.0f)
            {
                rb.angularVelocity += new Vector3(0, -rotateSpeed, 0);
                rbP.angularVelocity += new Vector3(-rotateSpeed, 0, 0) * 0.1f;
            }
        }
        else if (Input.GetKey(KeyCode.None))
        {
            rb.angularVelocity = Vector3.zero;
            rbP.angularVelocity = Vector3.zero;
        }
    }
}
