using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySystem : MonoBehaviour {

    public GameObject earth;
    public float coeff_G;

    private Rigidbody rb;
    private Vector3 gravityForce;
    private float moonMass, earthMass;
    private Vector3 distance;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    }

    private void FixedUpdate()
    {
        rb = GetComponent<Rigidbody>();
        moonMass = GetComponent<CelestialProperties>().GetMass();
        earthMass = earth.GetComponent<CelestialProperties>().GetMass();
        distance = (earth.transform.position - transform.position);

        gravityForce = coeff_G * moonMass * earthMass / Mathf.Pow(distance.magnitude, 2) * distance.normalized;

        rb.AddForce(gravityForce);
    }
}
