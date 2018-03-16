using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineScript : MonoBehaviour {

    public float enginePower;

    private Rigidbody rb;


	// Use this for initialization
	void Start () {
        rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.P))
        {
            rb.AddForce(-transform.up * enginePower);
        }
	}
}
