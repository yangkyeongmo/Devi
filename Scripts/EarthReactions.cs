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
    private List<GameObject> laserSatellites;

	// Use this for initialization
	void Start () {
        timeNow = (int)Time.time;
        timeNext = timeNow;
        laserSatellites = new List<GameObject>();
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
    
    void FindSatellites()
    {
        laserSatellites.Add(transform.Find("LaserSatellite").gameObject);
    }
}
