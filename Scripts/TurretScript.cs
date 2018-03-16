using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour {

    public GameObject shot;
    public float shootInterval;
    public float shotSpeed;
    
    private bool isSettled = false;
    private float nextTime;
    

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isSettled) Shoot();
    }

    void Shoot()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(Time.realtimeSinceStartup >= nextTime)
            {
                nextTime = Time.realtimeSinceStartup + shootInterval;
                GameObject shot = Instantiate(this.shot, gameObject.transform.position + transform.up * 3.0f, Quaternion.LookRotation(transform.forward));
                Rigidbody rb = shot.GetComponent<Rigidbody>();
                rb.velocity = shotSpeed * transform.up;
            }
        }
    }

    public void TurretIsInitialized(string message)
    {
        if (message.Equals("TurretInitialized")) isSettled = true;
    }
}
