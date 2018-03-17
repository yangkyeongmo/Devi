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
    public Vector3 headPosition;
    public float allowance;

    private Transform earth;
    private Transform player;
    private Transform destinationZoneTransform;
    private Vector3 preDestinationZonePosition;
    private Vector3 playerPosition;
    private Vector3 crossVector;
    private float distanceFromEarth;
    private float distanceFromPlayer;
    private Rigidbody rb;
    private float speed = 0;
    private Vector3 direction;
    private Vector3 detourDirection;
    private LineRenderer lr;

    private bool isBombarding = false;

    private GameObject debugMarker;

    // Use this for initialization
    void Start () {
        earth = GameObject.Find("Earth").transform;
        player = GameObject.FindWithTag("Player").transform;

        playerPosition = GameObject.FindWithTag("Player").transform.position;
        transform.up = (transform.position - earth.position).normalized;
        destinationZoneTransform = GameObject.Find("MidPoint" + Random.Range(0, 50)).transform;
        Debug.Log("Destination of " + gameObject.name + "is " + destinationZoneTransform.name + ", " + (playerPosition - destinationZoneTransform.position));
        preDestinationZonePosition = (playerPosition + (destinationZoneTransform.position - playerPosition) * 3.5f);
        crossVector = Vector3.Cross(transform.up, playerPosition - destinationZoneTransform.position);
        Debug.Log("CrossVector is " + crossVector);

        rb = GetComponent<Rigidbody>();

        //For Debug
        debugMarker = GameObject.Instantiate(marker, preDestinationZonePosition, Quaternion.identity);
        debugMarker.transform.parent = GameObject.FindWithTag("Player").transform;

        lr = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        preDestinationZonePosition = (playerPosition + (destinationZoneTransform.position - playerPosition) * 3.5f);
        playerPosition = player.position;
        distanceFromEarth = (transform.position - earth.position).magnitude;
        if ((transform.position - preDestinationZonePosition).magnitude > 8.0f && isBombarding == false)
        {
            SetSpeed();
            SimpleDetourAIByDistance(preDestinationZonePosition);
            SetDirection();
        }
        else if ((transform.position - preDestinationZonePosition).magnitude < 8.0f)
            Bombard();

        rb.velocity = speed * direction;

        //leave marker to visualize trajectory(for debug)
        GameObject mark = Instantiate(marker, transform);
        mark.transform.parent = GameObject.Find("StaticParent").transform;

        //use line to see where's its destination(for debug)
        lr.SetPosition(0, transform.position + transform.up * 2);
        lr.SetPosition(1, preDestinationZonePosition);
    }

    void SetSpeed()
    {
        distanceFromPlayer = (transform.position - playerPosition).magnitude;
        if(distanceFromEarth < initializingRange && speed < minSpeed)
        {
            speed += accelerateRate;
        }
        else if(distanceFromPlayer > rb.velocity.magnitude * allowance && speed < maxSpeed)
        {
            speed += accelerateRate;
        }
        else if (distanceFromPlayer < rb.velocity.magnitude * allowance && speed < approachSpeed)
        {
            speed -= accelerateRate;
        }
    }

    void SimpleDetourAIByDistance(Vector3 target)
    {
        if (distanceFromPlayer < rb.velocity.magnitude * allowance)
        {
            if (true) //if (hit == some object it should avoid)
            {
                detourDirection = Vector3.Cross(player.transform.up, transform.position - player.transform.position).normalized;
                if(Mathf.Abs(target.y - transform.position.y) < 1)
                {
                    detourDirection += ((target.y - transform.position.y) * Vector3.up).normalized;
                }
                detourDirection = detourDirection.normalized;
                
            }
        }
        else detourDirection = Vector3.zero;
    }

    void SetDirection()
    {
        if (distanceFromEarth < initializingRange)
        {
            direction = transform.up;
        }

        else if (distanceFromEarth > initializingRange)
        {
            if (distanceFromPlayer > rb.velocity.magnitude * allowance)
            {
                if (direction != (transform.up + (preDestinationZonePosition - transform.position).normalized * turnModifier).normalized)
                {
                    direction += (preDestinationZonePosition - transform.position).normalized * turnModifier; // slowly turn direction
                }
            }
        }

        transform.up = direction;

        /*
         if (direction != (transform.up + 0.5f * detourDirection.normalized).normalized)
        {
            direction += detourDirection * turnModifier; // slowly turn direction
        }
        */

        if (direction != transform.up + detourDirection * 10 / (transform.position - preDestinationZonePosition).magnitude)
        {
            direction += detourDirection * 10 / (transform.position - preDestinationZonePosition).magnitude * turnModifier;
        }

        transform.up = direction;

        direction = direction.normalized;
    }

    void Bombard()
    {
        isBombarding = true;
        direction = (player.position - transform.position).normalized;
        transform.up = direction;
        speed = maxSpeed * 2;
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

    private void OnDestroy()
    {
        Destroy(debugMarker);
    }
}
