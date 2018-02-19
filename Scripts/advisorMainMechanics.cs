using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvisorMainMechanics : MonoBehaviour {

    public float moveSpeed;

    private AdvisorMovement am;
    private bool isCollisionEntered = false;
    private GameObject player;
    private Vector3 moveDirection;

	// Use this for initialization
	void Start () {
        am = transform.parent.gameObject.GetComponent<AdvisorMovement>();
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (am.GetIsArrived())
        {
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            isCollisionEntered = true;
        }
    }
}
