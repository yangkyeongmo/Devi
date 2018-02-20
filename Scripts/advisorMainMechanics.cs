using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvisorMainMechanics : MonoBehaviour {

    public float detachSpeed;
    public float moveSpeed;

    private AdvisorMovement am;
    private bool isCollisionEntered = false;
    private GameObject player;
    private GameObject[] detachParts;
    private Vector3 moveDirection;
    private Vector3[] randomDirection;
    private Vector3[] randomRotationVector;

	// Use this for initialization
	void Start () {
        am = transform.parent.gameObject.GetComponent<AdvisorMovement>();
        player = GameObject.FindWithTag("Player");

        detachParts = new GameObject[6];
        detachParts[0] = transform.parent.Find("advisor_wall0").gameObject;
        detachParts[1] = transform.parent.Find("advisor_wall1").gameObject;
        detachParts[2] = transform.parent.Find("advisor_wall2").gameObject;
        detachParts[3] = transform.parent.Find("advisor_wall3").gameObject;
        detachParts[4] = transform.parent.Find("advisor_head").gameObject;
        detachParts[5] = transform.parent.Find("advisor_thrust").gameObject;

        randomDirection = new Vector3[6];
        for(int i=0; i<randomDirection.Length; i++)
        {
            randomDirection[i] = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        }
        randomRotationVector = new Vector3[6];
        for (int i = 0; i < randomRotationVector.Length; i++)
        {
            randomRotationVector[i] = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (am.GetIsArrived())
        {
            DetachParts();
            
            if (isCollisionEntered == false)
            {
                moveDirection = (player.transform.position - transform.position).normalized;
                transform.position += moveDirection * moveSpeed;
            }
            if (isCollisionEntered)
            {
                //sustain eccentricity of moon between 0.2 and 0.5
                //if apoapsis is too far, lower moon's velocity near closest point
                //if closest point is too far, lower moon's velocity near apoapsis
                //for both, if too close, give more velocity
                //modify direction to allowed range
                //for 10 sec
            }
        }
	}

    void DetachParts()
    {
        for (int i = 0; i < detachParts.Length; i++)
        {
            if(detachParts[i].transform.parent != null)
            {
                detachParts[i].transform.parent = null;
            }

            detachParts[i].transform.position += randomDirection[i] * detachSpeed;          //Detach useless parts
            Debug.Log(detachParts[i].transform.forward * detachSpeed);
            detachParts[i].transform.Rotate(randomRotationVector[i]);
            Debug.Log("Detaching..");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            isCollisionEntered = true;
            Debug.Log("Advisor main arrived!");
        }
    }
}
