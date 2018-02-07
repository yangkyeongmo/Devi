using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float planetZoom, minZoom, maxZoom;
    public float zoomSpeed;
    public float clampSpeed;
    public float clampMargin;
    public float cameraSpeed;

    private float scroll;
    private Camera cam;
    private GameObject player;
    private bool isPlayerClicked = false;
    private RaycastHit hit;
    private Ray ray;

    // Use this for initialization
    void Start () {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {

        ray = cam.ScreenPointToRay(Input.mousePosition);

        //Mouse ScrollWheel Control
        scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        Debug.Log("scroll: " + scroll);
        if (scroll != 0.0f)
        {
            cam.orthographicSize -= scroll;
            Debug.Log("zooming\nsize: " + cam.orthographicSize);
        }
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);

        //Move Camera to center if moon is clicked
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.name == "mouseHolder" && Input.GetMouseButtonDown(0) && cam.orthographicSize == minZoom)
            {
                isPlayerClicked = true;
            }
        }
        
        if (isPlayerClicked)
        {
            if (cam.transform.position.x != player.transform.position.x && cam.transform.position.y != player.transform.position.y)
            {
                Vector3 diff = player.transform.position - cam.transform.position + new Vector3(0, 5, 0);
                cam.transform.position += diff.normalized * clampSpeed;
                cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, -40);
                if (cam.transform.position.x < player.transform.position.x + clampMargin && cam.transform.position.x > player.transform.position.x - clampMargin &&
                    cam.transform.position.y < player.transform.position.y + 25 + clampMargin && cam.transform.position.y < player.transform.position.y + 25 - clampMargin)
                {
                    cam.transform.position = player.transform.position + new Vector3(0, 5, -40);
                }
            }

            if(cam.transform.position == (player.transform.position + new Vector3(0, 5, -40)))
            {
                isPlayerClicked = false;
            }
        }
            
        //Move Camera Position
        if(Input.GetAxis("Horizontal") > 0.0f)
        {
            cam.transform.position += new Vector3(cameraSpeed, 0.0f, 0.0f);
        }
        if (Input.GetAxis("Horizontal") < 0.0f)
        {
            cam.transform.position += new Vector3(-cameraSpeed, 0.0f, 0.0f);
        }
        if (Input.GetAxis("Vertical") > 0.0f)
        {
            cam.transform.position += new Vector3(0.0f, +cameraSpeed, 0.0f);
        }
        if (Input.GetAxis("Vertical") < 0.0f)
        {
            cam.transform.position += new Vector3(0.0f, -cameraSpeed, 0.0f);
        }
    }
}
