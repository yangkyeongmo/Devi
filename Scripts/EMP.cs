using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : MonoBehaviour {

    private bool keyPressed = false;
    private Collider cd;

	// Use this for initialization
	void Start () {
        cd = GetComponent<Collider>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            keyPressed = true;
            cd.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (keyPressed == true)
        {
            if (other.tag != "Player")
            {
                Destroy(other.gameObject);
            }
            cd.enabled = false;
        }

    }
}
