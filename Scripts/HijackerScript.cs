using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HijackerScript : MonoBehaviour {

    public float redirectionSpeed;

    private List<GameObject> enemies;

	// Use this for initialization
	void Start ()
    {
        enemies = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Redirection();
            //DestroyEnemies();
        }
	}

    void Redirection()
    {
        if (enemies.Count != 0)
        {
            foreach (GameObject enemy in enemies)
            {
                Rigidbody rb = enemy.GetComponent<Rigidbody>();
                rb.velocity += (enemy.transform.position - transform.position).normalized * redirectionSpeed;
            }
        }
    }

    void DestroyEnemies()
    {
        if (enemies.Count != 0)
        {
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy.gameObject);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        enemies.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (enemies.Contains(other.gameObject))
            enemies.Remove(other.gameObject);
    }
}
