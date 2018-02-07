using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    private float scroll;
    private Camera cam;

	// Use this for initialization
	void Start () {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {

        //Mouse ScrollWhell Control
        scroll = Input.GetAxis("Mouse ScrollWheel") * 10;
        Debug.Log("scroll: " + scroll);
        if (scroll != 0.0f)
        {
            cam.orthographicSize += scroll;
            Debug.Log("zooming\nsize: " + cam.orthographicSize);
        }
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 30.0f, 275.0f);

        //Move Camera Position
        if(Input.GetAxis("Horizontal") != 0.0f)
        {
            cam.transform.position += new Vector3(0.1f, 0.0f, 0.0f);
        }
        if(Input.GetAxis("Vertical") != 0.0f)
        {
            cam.transform.position += new Vector3(0.0f, 0.1f, 0.0f);
        }
    }
}
