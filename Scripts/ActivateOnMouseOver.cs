using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnMouseOver : MonoBehaviour {

    MeshRenderer mr;

	// Use this for initialization
	void Start () {
        mr = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        mr.enabled = true;
        Debug.Log("Mouse entered " + gameObject.name);
    }

    private void OnMouseExit()
    {
        mr.enabled = false;
        Debug.Log("Mouse exited " + gameObject.name);
    }
}
