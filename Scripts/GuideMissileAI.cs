using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideMissileAI : MonoBehaviour {

    private bool isDetouring = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SimpleDetourAI(GameObject go)
    {
        float allowance = 3;
        Vector3 upward = go.transform.up;
        Rigidbody rb = go.GetComponent<Rigidbody>();
        RaycastHit hit;

        if (Physics.Raycast(transform.position, upward, out hit, rb.velocity.magnitude * allowance))
        {
            if (true) //if (hit == some object it should avoid)
            {
                isDetouring = true;
                Debug.Log(go.transform.name + " is detouring because of " + hit.transform.name);
                rb.velocity = rb.velocity.magnitude * Vector3.Cross(hit.transform.up, go.transform.position - hit.transform.position);
            }
        }
    }

    public bool GetIsDetouring()
    {
        return isDetouring; //to take control of other script's decision on velocity
    }
}
