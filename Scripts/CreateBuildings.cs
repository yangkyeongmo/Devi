using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//In this script, you create buildings on moon's surface zones, set which zone is occupied and not occupied, and give access to other scripts which zone is occupied or not.

public class CreateBuildings : MonoBehaviour
{
    public GameObject railgunTurret;

    private bool isTurretButtonClicked = false;
    private bool isSetZoneOccupiedArray = false;

    private GameObject player;
    private newDivideZones dz;
    private int[] zoneOccupied;

    // Use this for initialization
    void Start()
    {
        player = this.gameObject;
        dz = GetComponent<newDivideZones>();
        zoneOccupied = new int[50];
        for(int i=0; i < zoneOccupied.Length; i++)
        {
            zoneOccupied[i] = 0;
        }
        isSetZoneOccupiedArray = true;
        Debug.Log("Completed setting zoneOccupied");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurretButtonClicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                int selectedZone = dz.GetSelectedZoneNumber();
                GameObject selectedMidPoint = GameObject.Find("MidPoint" + selectedZone);
                Debug.Log(selectedMidPoint.name);
                GameObject spawnedTurret = Instantiate(railgunTurret, selectedMidPoint.transform.position, Quaternion.identity);
                spawnedTurret.transform.parent = selectedMidPoint.transform;
                spawnedTurret.transform.up = - player.transform.position + spawnedTurret.transform.position;
                spawnedTurret.transform.position -= spawnedTurret.transform.up * 3.5f;
                SetZoneOccupied(selectedZone);
                isTurretButtonClicked = false;
            }
        }
    }

    private void SetZoneOccupied(int num)
    {
        zoneOccupied[num] = 1;
    }

    public void SetTurretOnTrue()
    {
        isTurretButtonClicked = true;
    }

    public int[] GetOccupyInfo()
    {
        return zoneOccupied;
    }

    public bool GetIsSetZoneOccupiedArray()
    {
        return isSetZoneOccupiedArray;
    }
}