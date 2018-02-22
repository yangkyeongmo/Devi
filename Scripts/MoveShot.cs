using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShot : MonoBehaviour {

    public float speed;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = transform.up * speed;
	}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
        //decrease health of collision.gameobject
    }
}
