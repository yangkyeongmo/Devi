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
    private Vector3 preDestinationZonePosition;
    private Vector3 playerPosition;
    private Vector3 crossVector;
    private float distanceFromEarth;
    private float distanceFromPlayer;
    private Rigidbody rb;
    private float speed = 0;
    private Vector3 direction;
    private bool isDetouringToDestination = false;
    private bool isMovingUpward;
    private bool isMovingToPlayer;

    private GameObject debugMarker;

    // Use this for initialization
    void Start () {
        gm = GetComponent<GuideMissileAI>();
        earth = GameObject.Find("Earth").transform;

        playerPosition = GameObject.FindWithTag("Player").transform.position;
        transform.up = (transform.position - earth.position).normalized;
        destinationZoneTransform = GameObject.Find("MidPoint" + Random.Range(0, 50)).transform;
        preDestinationZonePosition = (playerPosition + (destinationZoneTransform.position - playerPosition) * 3.5f);
        Debug.Log("Destination of " + gameObject.name + "is " + destinationZoneTransform.name + ", " + (playerPosition - destinationZoneTransform.position));
        gm.SetTarget(preDestinationZonePosition);
        crossVector = Vector3.Cross(transform.up, playerPosition - destinationZoneTransform.position);
        Debug.Log("CrossVector is " + crossVector);

        rb = GetComponent<Rigidbody>();

        //For Debug
        debugMarker = GameObject.Instantiate(marker, preDestinationZonePosition, Quaternion.identity);
        debugMarker.transform.parent = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerPosition = GameObject.FindWithTag("Player").transform.position;
        distanceFromEarth = (transform.position - earth.position).magnitude;
        SetSpeed();
        SetDirection();
        rb.velocity = speed * direction;

        //leave marker to visualize trajectory(for debug)
        GameObject mark = Instantiate(marker, transform);
        mark.transform.parent = GameObject.Find("StaticParent").transform;
    }

    void SetSpeed()
    {
        distanceFromPlayer = (transform.position - playerPosition).magnitude;
        if(distanceFromEarth < initializingRange && speed < minSpeed)
        {
            speed += accelerateRate;
        }
        else if(distanceFromPlayer > rb.velocity.magnitude * gm.GetAllowance() && speed < maxSpeed)
        {
            speed += accelerateRate;
        }
        else if (distanceFromPlayer < rb.velocity.magnitude * gm.GetAllowance() && speed < approachSpeed)
        {
            speed -= accelerateRate;
        }
    }

    void SetDirection()
    {
        if (distanceFromEarth < initializingRange)
        {
            direction = transform.up;
        }

        else if (distanceFromEarth > initializingRange)
        {

            if (distanceFromPlayer > deceleratingRange)
            {
                if (direction != (transform.up + (playerPosition - transform.position).normalized * turnModifier).normalized
                //direction.x <= (transform.up + (playerPosition - transform.position).normalized * modifier).normalized.x
                //&& direction.y <= (transform.up + (playerPosition - transform.position).normalized * modifier).normalized.y
                //&& direction.z <= (transform.up + (playerPosition - transform.position).normalized * modifier).normalized.z)
                )
                {
                    direction += (playerPosition - transform.position).normalized * turnModifier;
                }
            }
            else if (distanceFromPlayer < deceleratingRange)
            {
                isDetouringToDestination = true;
                transform.parent = GameObject.FindWithTag("Player").transform;
            }
        }
        /*DetourToDestination();
        if(distanceFromPlayer > deceleratingRange)
        {
            isDetouringToDestination = false;
        }*/
        direction += 5 * gm.GetDirection();
        transform.up = direction;

        direction = direction.normalized;
    }

    void DetourToDestination()
    {
        /*Debug.Log(isMovingUpward);
        preDestinationZonePosition = (playerPosition + (destinationZoneTransform.position - playerPosition) * 3.5f);
        if (isMovingUpward == false)
        {
            if (Mathf.Abs((transform.position.x - preDestinationZonePosition.x)) > 5.0f
            && Mathf.Abs((transform.position.z - preDestinationZonePosition.z)) > 5.0f)                                               //stage 1
            {
                if (direction != Vector3.Cross(transform.position - playerPosition, Vector3.up))
                {
                    direction += Vector3.Cross(transform.position - playerPosition, Vector3.up).normalized * turnModifier;
                }
                Debug.Log("distance stage 1, x: " + Mathf.Abs((transform.position.x - preDestinationZonePosition.x)));
                Debug.Log("distance stage 1, z: " + Mathf.Abs((transform.position.z - preDestinationZonePosition.z)));
            }
            else if (Mathf.Abs((transform.position.x - preDestinationZonePosition.x)) < 5.0f                                               //stage 2
                && Mathf.Abs((transform.position.z - preDestinationZonePosition.z)) < 5.0f)
            {
                isMovingUpward = true;
            }
        }

        else if (isMovingUpward == true && isMovingToPlayer == false)
        {
            if ((transform.position - preDestinationZonePosition).magnitude > 2.0f)
            {
                direction += Vector3.up * turnModifier;
                Debug.Log("Direction: " + direction);
            }
            else if ((transform.position - preDestinationZonePosition).magnitude < 2.0f)
            {
                Debug.Log("Move To Player");
                isMovingToPlayer = true;
            }
        }

        else if (isMovingToPlayer)
        {
            //if (transform.up != (playerPosition - transform.position))                                                                                   //turn toward destination
            //{
                direction = (transform.position - playerPosition).normalized * turnModifier * 2;
            //}
        }*/

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
