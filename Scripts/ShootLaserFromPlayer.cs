using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaserFromPlayer : MonoBehaviour {

    private LineRenderer line;
    private GameObject confrontedEnemy;
    private ParticleSystem laserSpark;

	// Use this for initialization
	void Start () {
        line = GetComponent<LineRenderer>();
        line.enabled = false;
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
        while (confrontedEnemy != null)
        {
            Ray ray = new Ray(transform.position, transform.position - confrontedEnemy.transform.position);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, confrontedEnemy.transform.position);
            
            yield return null;
        }
        line.enabled = false;
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
