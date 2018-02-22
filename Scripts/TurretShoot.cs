using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour {

    public GameObject shot;
    public float shootInterval;
    public float shotSpeed;

    private bool spacePressed;
    private float firstValue, nextValue;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            spacePressed = true;
            if (spacePressed)
            {
                if(Time.time >= nextValue)
                {
                    Shoot();
                    nextValue += shootInterval;
                }
            }
            Debug.Log("Called after shooting");
        }
        else if (Input.GetKey(KeyCode.Space) != true)
        {
            spacePressed = false;
        }
    }

    void Shoot()
    {
        GameObject instshot = Instantiate(shot, transform.position + transform.up * 3.5f, Quaternion.LookRotation(transform.up));
        instshot.transform.up = transform.up;
        Rigidbody rb = instshot.GetComponent<Rigidbody>();
        rb.velocity = transform.up * shotSpeed;
    }
}
