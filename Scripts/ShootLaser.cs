using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour {

    public float destructionRate;
    
    private LineRenderer line;
    private GameObject confrontedEnemy;
    //private GameObject laserSpark;

	// Use this for initialization
	void Start () {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        //laserSpark = transform.Find("LaserSpark").gameObject;
        //laserSpark.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
        if (confrontedEnemy != null)
        {
            StopCoroutine("ShootToEnemy");
            StartCoroutine("ShootToEnemy");
        }
    }

    IEnumerator ShootToEnemy()
    {
        line.enabled = true;
        //laserSpark.SetActive(true);
        while (confrontedEnemy != null)
        {
            Ray ray = new Ray(transform.position, transform.position - confrontedEnemy.transform.position);
            RaycastHit hit;
            line.SetPosition(0, transform.position);

            /*if(Physics.Raycast(ray, out hit, 10))
            {
                if(hit.transform.tag != "Player")
                {
                    line.SetPosition(1, hit.transform.position);
                    Debug.Log(hit.transform.name);
                    //laserSpark.transform.position = hit.transform.position;
                    Rigidbody rb = hit.rigidbody;
                    rb.mass -= destructionRate;
                }
            }
            else
            {*/
                line.SetPosition(1, confrontedEnemy.transform.position);
            //}
            
            yield return null;
        }
        line.enabled = false;
        //laserSpark.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(confrontedEnemy == null)
        {
            confrontedEnemy = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (confrontedEnemy == null)
        {
            confrontedEnemy = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == confrontedEnemy)
        {
            confrontedEnemy = null;
        }
    }

}
