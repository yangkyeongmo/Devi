using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySystem : MonoBehaviour {

    public GameObject earth;
    public float coeff_G;
    public Vector3 initialSpeedDirection;
    public float initialSpeed;
    public GameObject marker;

    private Rigidbody rb;
    private Vector3 gravityForce;
    private float moonMass, earthMass;
    private Vector3 distance;
    private GameObject markerParent;
    private float firstTime, nextTime;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = initialSpeedDirection.normalized * initialSpeed;

        markerParent = new GameObject();
        markerParent.transform.name = "OrbitMarkers";
    }
	
	// Update is called once per frame
	void Update ()
    {//Create marker for debugging
        firstTime = (int)System.Math.Truncate(Time.time / 1);
        if (firstTime == nextTime)
        {
            var i_marker = Instantiate(marker, transform.position, Quaternion.identity);
            i_marker.transform.parent = markerParent.transform;

            nextTime = firstTime + 1;
        }
    }

    private void FixedUpdate()
    {
        moonMass = GetComponent<CelestialProperties>().GetMass();
        earthMass = earth.GetComponent<CelestialProperties>().GetMass();
        distance = (earth.transform.position - transform.position);

        gravityForce = coeff_G * moonMass * earthMass / Mathf.Pow(distance.magnitude, 2) * distance.normalized;
        //gravityForce = coeff_G * 5 / Mathf.Pow(distance.magnitude, 2) * distance.normalized;

        rb.AddForce(gravityForce);
    }
}
