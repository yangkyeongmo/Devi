using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretAnimation : MonoBehaviour {

    public float speed;
    public float endValue;

    private float startValue = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        while(startValue != endValue)
        {
            transform.position += transform.up * speed * Time.deltaTime;
            startValue += speed;
        }
	}
}
