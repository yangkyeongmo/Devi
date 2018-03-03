using System.Collections;
using UnityEngine;

public class OrbitalMechanics : MonoBehaviour {

    public GameObject marker;
    public Vector3 firstVelocityDirection;
    public float firstVelocityMagnitude;
    public GameObject largerMass;

    public float e;
    public float G;
    public int debug_updateTimes;

    private GameObject markerParent;
    private Vector3 first_velocity;
    private bool initialUpdate = true;
    private Vector3 i1, i2, i3;
    private Vector3 o1, o2, o3;
    private Vector3 init_position, init_velocity;
    private float p, i, w, v;
    private Vector3 h, vector_e;
    private float mu;
    private float next_v;
    private float E, M;
    private float k, a;
    private float next_M, next_E;
    private Vector3 next_position, next_velocity;
    private CelestialProperties lm_cp;
    private Rigidbody rb;
    private int firstTime, nextTime;

    private int updateTimes = 0;

	// Use this for initialization
	void Start () {

        markerParent = new GameObject();
        markerParent.transform.name = "OrbitMarkers";
        //initializing orthogonal vectors
        i1 = new Vector3(1, 0, 0);
        i2 = new Vector3(0, 1, 0);
        i3 = new Vector3(0, 0, -1);

        //set first velocity
        first_velocity = firstVelocityDirection.normalized * firstVelocityMagnitude;

        //get earth's mass
        lm_cp = largerMass.GetComponent<CelestialProperties>();

        //get satellite's velocity
        rb = GetComponent<Rigidbody>();
        rb.velocity = first_velocity;

        //set gravity coefficient
        //G = Mathf.Pow(10,3);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log("----------------------------------");
        PositionUpdateAlgorithm();  
        Debug.Log("----------------------------------");                                                                                    //move as its orbit if collision not detected
        //if(isCollisionDetected){                                                                                      //move as its new orbit if collision detected
        //blah blah... start from beginning and sustain its collision force
        //}
	}

    void PositionUpdateAlgorithm()
    {
        if (initialUpdate)
        {
            ConvertToOrbitalElements();
            Debug.Log("Converting Completed");
        }

        E = Mathf.Atan(Mathf.Sqrt((1 - e) / (1 + e)) * Mathf.Tan(v / 2)) * 2;
        M = E - e * Mathf.Sin(E);
        next_M = M + Mathf.Sqrt(mu / Mathf.Pow(a, 3)) * Time.deltaTime;
        k = 0;
        while(next_M >= 2 * Mathf.PI)
        {
            next_M -= 2 * Mathf.PI;
            k++;
        }
        for(int i=0; i<10; i++)
        {
            next_E = next_M + e * Mathf.Sin(E);
            E = next_E;
        }
        next_v = Mathf.Atan(Mathf.Sqrt((1 + e) / (1 - e)) * Mathf.Tan(next_E / 2)) * 2;
        v = next_v;
        Debug.Log("next v is: " + v);

        ConvertFromOrbitalElements();
        transform.position = next_position;
        rb.velocity = next_velocity;

        //Create marker for debugging
        firstTime = (int)System.Math.Truncate(Time.time / 1);
        if(firstTime == nextTime)
        {
            var i_marker = Instantiate(marker, transform.position, Quaternion.identity);
            i_marker.transform.parent = markerParent.transform;

            nextTime = firstTime + 1;
        }

        //if(updateTimes == debug_updateTimes)
            //initialUpdate = false;
        /*else if(updateTimes != debug_updateTimes)
        {
            Debug.Log("Velocity&Position after update: " + next_velocity + next_position);
            Debug.Log("Variables after update(p,w,v,e,vector_e: " + p + "/" + "/" + w + "/" + v + "/" + e + "/" + vector_e);
        }
        updateTimes++;*/
    }

    void ConvertToOrbitalElements()
    {
        //Debug.Log("Convert to OE Started");
        init_position = transform.position - largerMass.transform.position;
        init_velocity = rb.velocity;
        Debug.Log("Converting.. init_position: " + init_position);
        Debug.Log("Converting.. init_velocity: " + init_velocity);
        //h = Vector3.Cross(init_position, init_velocity);
        //manual calculation of h because of inaccurate cross product result
        h = new Vector3(0,0,init_position.x * init_velocity.y - init_position.y * init_velocity.x);
        Debug.Log("Converting.. h: " + h);
        //mu = G * lm_cp.GetMass();
        mu = G * 1;
        p = h.sqrMagnitude / mu;
        Debug.Log("Converting.. p: " + p);
        vector_e = ((init_velocity.sqrMagnitude - (mu / init_position.magnitude)) * init_position - Vector3.Dot(init_position, init_velocity) * init_velocity) / mu;
        Debug.Log("Converting.. vector_e: " + vector_e);
        e = vector_e.magnitude;
        Debug.Log("Converting.. e: " + e);
        a = p / (1 - Mathf.Pow(e, 2));
        Debug.Log("Converting.. a: " + a);
        //i = 0;

        o1 = vector_e.normalized;
        o3 = h.normalized;
        o2 = Vector3.Cross(o3, o1);

        /*w = Mathf.Acos(Vector3.Dot(i1, vector_e) / e);
        Debug.Log("Converting.. w: " + w);
        if (Vector3.Dot(i2, vector_e) < 0)
        {
            Debug.Log(Vector3.Dot(i2, vector_e));
            w = -w;
        }
        if(vector_e == Vector3.zero)
        {
            w = 0;
        }
        Debug.Log("Converting.. modified w: " + w);*/
        if (e != 0)
        {
            v = Mathf.Acos(Vector3.Dot(vector_e.normalized, init_position.normalized));
            if (Vector3.Dot(init_position, init_velocity) < 0)
            {
                v = -v;
            }
        }
        else if (e == 0)
        {
            v = Mathf.Acos(Vector3.Dot(i1, init_position) / init_position.magnitude);
            if (Vector3.Dot(i1, init_position) < 0)
            {
                v = -v;
            }
        }
        Debug.Log("Converting.. v: " + v);
    }

    void ConvertFromOrbitalElements()
    {
        //Debug.Log("Convert From OE Started");
        /*next_position = (p/(1+e* Mathf.Cos(v))) * (Mathf.Cos(v) * o1 + Mathf.Sin(v) * o2);
        Debug.Log("Converting.. next_position: " + next_position);
        next_velocity = Mathf.Sqrt(p / mu) * (-Mathf.Sin(v) * o1 + (e + Mathf.Cos(v)) * o2);
        Debug.Log("Converting.. next_velocity: " + next_velocity);*/

        float radiusMagnitude = p / (1 + e * Mathf.Cos(v));
        Debug.Log("Converting.. radius magnitude: " + radiusMagnitude);
        next_position = new Vector3(Mathf.Cos(v), Mathf.Sin(v), 0) * radiusMagnitude;
        //next_velocity = new Vector3(-Mathf.Sin(v), -Mathf.Cos(v), 0) * mu / h.magnitude * (1 + e);

        Vector3 ur = init_position.normalized;
        Vector3 uh = h.normalized;
        Vector3 unr = Vector3.Cross(uh, ur);

        next_velocity = (mu / h.magnitude) * (e * Mathf.Sin(v) * ur + (1 + e * Mathf.Cos(v))* unr);
    }
}
