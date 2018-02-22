using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (other.tag != "Player")
            {
                Destroy(other.gameObject);
            }
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

}
