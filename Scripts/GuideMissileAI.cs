using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideMissileAI : MonoBehaviour {

    public Vector3 headPosition;

    private bool isDetouring = false;
    private Vector3 direction;
    private float allowance;
    private Rigidbody rb;
    private Vector3 target;

	// Use this for initialization
	void Start ()
    {
        allowance = 10;
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        SimpleDetourAI(target);
	}

    void SimpleDetourAI(Vector3 target)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + headPosition , transform.up, out hit, rb.velocity.magnitude * allowance))
        {
            if (true) //if (hit == some object it should avoid)
            {
                isDetouring = true;
                Debug.Log(transform.name + " is detouring because of " + hit.transform.name);
                direction = Vector3.Cross(hit.transform.up, transform.position - hit.transform.position);
                direction += ((target.y - transform.position.y) * Vector3.up).normalized ;
            }
        }
    }

    public void SetTarget(Vector3 tf)
    {
        target = tf;
    }

    public bool GetIsDetouring()
    {
        return isDetouring; //to take control of other script's decision on velocity
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    public float GetAllowance()
    {
        return allowance;
    }
}
