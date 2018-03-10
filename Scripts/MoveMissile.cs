using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMissile : MonoBehaviour {

    public GameObject marker;
    public float destructionRate;
    public float detourRange, initializingRange, deceleratingRange;
    public float minSpeed, approachSpeed, maxSpeed;
    public float accelerateRate;
    public float turnModifier;

    private GuideMissileAI gm;
    private Transform earth;
    private Transform destinationZoneTransform;
    private Vector3 playerPosition;
    private float distanceFromEarth;
    private float distanceFromPlayer;
    private Rigidbody rb;
    private float speed = 0;
    private Vector3 direction;

    // Use this for initialization
    void Start () {
        gm = GetComponent<GuideMissileAI>();
        earth = GameObject.Find("Earth").transform;
        destinationZoneTransform = GameObject.Find("MidPoint" + Random.Range(0, 50)).transform;
        Debug.Log("Destination of " + gameObject.name + "is " + destinationZoneTransform.name);
        rb = GetComponent<Rigidbody>();
        transform.up = (transform.position - earth.position).normalized;
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerPosition = GameObject.FindWithTag("Player").transform.position;
        distanceFromEarth = (transform.position - earth.position).magnitude;
        SetSpeed();
        if (gm.GetIsDetouring() == false)
        {
            SetDirection();
            rb.velocity = speed * direction;
        }

        //leave marker to visualize trajectory(for debug)
        GameObject mark = Instantiate(marker, transform);
        mark.transform.parent = GameObject.Find("StaticParent").transform;
    }

    void SetSpeed()
    {
        distanceFromPlayer = (transform.position - playerPosition).magnitude;
        if(distanceFromEarth < initializingRange)
        {
            if(speed < minSpeed)
            {
                speed += accelerateRate;
            }
        }
        else if(distanceFromPlayer > deceleratingRange)
        {
            if(speed < maxSpeed)
            {
                speed += accelerateRate;
            }
        }
        else if (distanceFromPlayer < deceleratingRange)
        {
            if (speed < approachSpeed)
            {
                speed -= accelerateRate;
            }
        }
    }

    void SetDirection()
    {
        if (distanceFromEarth < initializingRange)
        {
            direction = transform.up;
        }

        if (distanceFromEarth > initializingRange)
        {

            if (direction != (transform.up + (playerPosition - transform.position).normalized * turnModifier).normalized
                //direction.x <= (transform.up + (playerPosition - transform.position).normalized * modifier).normalized.x
                //&& direction.y <= (transform.up + (playerPosition - transform.position).normalized * modifier).normalized.y
                //&& direction.z <= (transform.up + (playerPosition - transform.position).normalized * modifier).normalized.z)
                )
            {
                direction += (playerPosition - transform.position).normalized * turnModifier;
            }
            transform.up = direction;
        }

        direction = direction.normalized;
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
