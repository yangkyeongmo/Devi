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
            Ray ray = new Ray(transform.position, confrontedEnemy.transform.position - transform.position);
            RaycastHit hit;
            line.SetPosition(0, transform.position);

            if(Physics.Raycast(ray, out hit, 10))
            {
                line.SetPosition(1, hit.transform.position);
                //laserSpark.transform.position = hit.transform.position;
                Rigidbody rb = hit.rigidbody;
                rb.mass -= destructionRate;
            }
            else
            {
                line.SetPosition(1, confrontedEnemy.transform.position);
            }
            
            yield return null;
        }
        line.enabled = false;
        //laserSpark.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        AddEnemy(other);
    }

    private void OnTriggerStay(Collider other)
    {
        AddEnemy(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == confrontedEnemy)
        {
            confrontedEnemy = null;
        }
    }

    void AddEnemy(Collider other)
    {
        if (confrontedEnemy == null)
        {
            if (other.name != "Earth" && other.tag != "Player")
                confrontedEnemy = other.gameObject;
        }
    }
}
