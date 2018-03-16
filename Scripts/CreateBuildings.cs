using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//In this script, you create buildings on moon's surface zones, set which zone is occupied and not occupied, and give access to other scripts which zone is occupied or not.

public class CreateBuildings : MonoBehaviour
{
    public GameObject railgunTurret;
    public GameObject defenseTurret;
    public GameObject hijacker;
    public GameObject engine;

    private bool isBuildingButtonClicked = false;
    private bool isTurretButtonClicked = false;
    private bool isDefenseTurretButtonClicked = false;
    private bool isHijackerButtonClicked = false;
    private bool isEngineButtonClicked = false;
    private bool isSetZoneOccupiedArray = false;

    private GameObject player;
    private newDivideZones dz;
    private int[] zoneOccupied;
    private int selectedZone;
    private GameObject selectedMidPoint;

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
        DeployBuilding();
    }

    void DeployBuilding()
    {
        if (isBuildingButtonClicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (dz.GetIsClickedOnPlayer())
                {
                    selectedZone = dz.GetSelectedZoneNumber();
                    selectedMidPoint = GameObject.Find("MidPoint" + selectedZone);
                    Debug.Log("Building created on " + selectedMidPoint.name);

                    if (zoneOccupied[selectedZone] == 0)
                    {
                        GameObject spawnedBuilding;

                        if (isTurretButtonClicked)
                            spawnedBuilding = Instantiate(railgunTurret, selectedMidPoint.transform.position, Quaternion.identity);
                        else if (isDefenseTurretButtonClicked)
                            spawnedBuilding = Instantiate(defenseTurret, selectedMidPoint.transform.position, Quaternion.identity);
                        else if (isHijackerButtonClicked)
                            spawnedBuilding = Instantiate(hijacker, selectedMidPoint.transform.position, Quaternion.identity);
                        else if (isEngineButtonClicked)
                            spawnedBuilding = Instantiate(engine, selectedMidPoint.transform.position, Quaternion.identity);
                        else
                        {
                            spawnedBuilding = new GameObject();
                            Debug.Log("Null Spawned Building");
                        }
                        spawnedBuilding.transform.parent = selectedMidPoint.transform;
                        spawnedBuilding.transform.up = -player.transform.position + spawnedBuilding.transform.position;
                        //spawnedBuilding.transform.position -= spawnedBuilding.transform.up * 3.5f;

                        SetZoneOccupied(selectedZone);
                        isTurretButtonClicked = false;
                        isDefenseTurretButtonClicked = false;
                        isBuildingButtonClicked = false;
                        Debug.Log("Building is " + spawnedBuilding.name);
                    }
                    else if (zoneOccupied[selectedZone] == 1)
                        Debug.Log("Selected zone is already occupied by another building");
                    else
                        Debug.Log("Unknown zoneOccupied[] ERROR");
                }

                else if (dz.GetIsClickedOnPlayer() == false)
                    Debug.Log("Can't build: Not Clicked on Player");
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
        isBuildingButtonClicked = true;
    }

    public void SetDefenseTurretOnTrue()
    {
        isDefenseTurretButtonClicked = true;
        isBuildingButtonClicked = true;
    }

    public void SetHijackerOnTrue()
    {
        isHijackerButtonClicked = true;
        isBuildingButtonClicked = true;
    }

    public void SetEngineOnTrue()
    {
        isEngineButtonClicked = true;
        isBuildingButtonClicked = true;
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