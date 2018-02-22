using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour {

    public GameObject shot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject instshot = Instantiate(shot, transform.position + transform.up * 35, Quaternion.LookRotation(transform.up));
        }
	}
}
