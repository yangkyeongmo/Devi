using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    public GameObject earth;
    public GameObject moon;
    private CelestialProperties cp_earth;
    private CelestialProperties cp_moon;

    public float satelliteVelocity;
    public float satelliteMass;
    public float planetVelocity;
    public float planetMass;
    public Text dataText;

	// Use this for initialization
	void Start ()
    {
        cp_earth = earth.GetComponent<CelestialProperties>();
        cp_moon = moon.GetComponent<CelestialProperties>();

            //set velocity&mass of objects
        //satelliteVelocity = 0.8f; satelliteMass = 2f;
        //planetVelocity = 0; planetMass = 10f;
        SetCelestialProperties(moon, satelliteVelocity, satelliteMass);
        SetCelestialProperties(earth, planetVelocity, planetMass);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //earth.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        dataText.text = "MOON \n Velocity: " + cp_moon.GetVelocity() + "\n Mass: " + cp_moon.GetMass() +
            "\n\n EARTH \n Velocity: " + cp_earth.GetVelocity() + "\n Mass: " + cp_earth.GetMass();

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Time.timeScale = 1;
            Debug.Log("Time x1");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Time.timeScale = 3;
            Debug.Log("Time x3");
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Time.timeScale = 5;
            Debug.Log("Time x5");
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            Time.timeScale = 15;
            Debug.Log("Time x15");
        }
    }

    void SetCelestialProperties(GameObject body, float v, float m)
    {
        CelestialProperties cp = body.GetComponent<CelestialProperties>();
        cp.SetVelocity(v);
        cp.SetMass(m);
    }
}
