using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//In this script, you create buildings on moon's surface zones, set which zone is occupied and not occupied, and give access to other scripts which zone is occupied or not.

public class CreateBuildings : MonoBehaviour
{
    public GameObject railgunTurret;

    private int[] zoneOccupied;
    private bool isTurretButtonClicked = false;
    private newDivideZones dz;

    // Use this for initialization
    void Start()
    {
        dz = GetComponent<newDivideZones>();
        zoneOccupied = new int[50];
        for(int i=0; i < zoneOccupied.Length; i++)
        {
            zoneOccupied[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurretButtonClicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                int selectedZone = dz.GetSelectedZoneNumber();
                Debug.Log("selected Zone: " + selectedZone);
                GameObject spawnedTurret = Instantiate(railgunTurret, 
                    GameObject.Find("MidPoint" + selectedZone).transform.position, 
                    Quaternion.identity);
                spawnedTurret.transform.parent = this.gameObject.transform;
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

}
