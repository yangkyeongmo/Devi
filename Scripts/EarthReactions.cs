using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthReactions : MonoBehaviour {

    public Transform[] launchPosition;
    public GameObject attackMissile;
    public int missileSpawnInterval;

    private Transform earthCore;
    private Transform selectedLaunchPosition;
    private int timeNow;
    private int timeNext;

	// Use this for initialization
	void Start () {
        earthCore = GameObject.Find("earthCore").transform;
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
        Vector3 direction;
        int randomCount = Random.Range(0, launchPosition.Length - 1);
        selectedLaunchPosition = launchPosition[randomCount];
        Debug.Log("Attack Missile Launch at : Position" + randomCount);

        direction = (earthCore.position - selectedLaunchPosition.position).normalized;
        GameObject spawnedMissile = Instantiate(attackMissile, selectedLaunchPosition.position , Quaternion.identity);
        spawnedMissile.transform.up = direction;
        Debug.Log("Attack Missile Launched");
    }
}
