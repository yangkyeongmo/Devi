using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMissile : MonoBehaviour {

    public float initialSpeed;
    public float modifier;
    public GameObject marker;
    public float destructionRate;
    public float detourRange;

    private GuideMissileAI gm;
    private Transform earth;
    private Transform destinationZoneTransform;
    private Vector3 playerPosition;
    private float distanceFromEarth;
    private Rigidbody rb;
    private float speed;
    private Vector3 direction;

    // Use this for initialization
    void Start () {
        gm = GetComponent<GuideMissileAI>();
        earth = GameObject.Find("Earth").transform;
        destinationZoneTransform = GameObject.Find("MidPoint" + Random.Range(0, 50)).transform;
        Debug.Log("Destination of " + gameObject.name + "is " + destinationZoneTransform.name);
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * initialSpeed;
        transform.up = (transform.position - earth.position).normalized;
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerPosition = GameObject.FindWithTag("Player").transform.position;
        distanceFromEarth = (transform.position - earth.position).magnitude;
        speed = rb.velocity.magnitude;
        if (gm.GetIsDetouring() == false)
        {
            if (distanceFromEarth < 25.0f)
            {
                direction = transform.up;
            }

            if (distanceFromEarth > 25.0f)
            {

                if (direction != (transform.up + (playerPosition - transform.position).normalized * modifier).normalized
                    //direction.x <= (transform.up + (playerPosition - transform.position).normalized * modifier).normalized.x
                    //&& direction.y <= (transform.up + (playerPosition - transform.position).normalized * modifier).normalized.y
                    //&& direction.z <= (transform.up + (playerPosition - transform.position).normalized * modifier).normalized.z)
                    )
                {
                    direction += (playerPosition - transform.position).normalized * modifier;
                }
                transform.up = direction;
            }

            direction = direction.normalized;
            rb.velocity = speed * direction;
        }

        //leave marker to visualize trajectory(for debug)
        GameObject mark = Instantiate(marker, transform);
        mark.transform.parent = GameObject.Find("StaticParent").transform;
    }

    void DetourToDestination()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            Destroy(this.gameObject);
            Rigidbody rb = collision.rigidbody;
            rb.mass -= destructionRate;
        }
    }
}
