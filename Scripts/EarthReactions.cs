using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthReactions : MonoBehaviour {

    public Transform[] launchPosition;
    public GameObject attackMissile;
    public int missileSpawnInterval;
    
    private Transform selectedLaunchPosition;
    private int timeNow;
    private int timeNext;

	// Use this for initialization
	void Start () {
        timeNow = (int)Time.time;
        timeNext = timeNow;
	}
	
	// Update is called once per frame
	void Update () {
        timeNow = (int)Time.time;
        if (timeNow == timeNext)
        {
            timeNext = timeNow + missileSpawnInterval;
            SpawnMissile();
        }
	}

    void SpawnMissile()
    {
        int randomCount = Random.Range(0, launchPosition.Length - 1);
        selectedLaunchPosition = launchPosition[randomCount];
        Instantiate(attackMissile, selectedLaunchPosition.position, Quaternion.identity);
        Debug.Log("Attack Missile Launched at : Position" + randomCount);
    }
}
