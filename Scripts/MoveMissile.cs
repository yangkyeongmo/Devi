using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMissile : MonoBehaviour {

    public float initialSpeed;
    public float modifier;
    public GameObject marker;

    private Vector3 playerPosition;
    private Rigidbody rb;
    private float speed;
    private Vector3 direction;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * initialSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        playerPosition = GameObject.FindWithTag("Player").transform.position;
        speed = rb.velocity.magnitude;
        direction = (transform.up + (playerPosition - transform.position).normalized * modifier).normalized;
        transform.up = direction;
        rb.velocity = speed * direction;
        GameObject mark = Instantiate(marker, transform);
        mark.transform.parent = GameObject.Find("StaticParent").transform;
    }
}
