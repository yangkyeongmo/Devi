using System.Collections;
using UnityEngine;

public class OrbitalMechanics : MonoBehaviour {

    public GameObject marker;
    public Vector3 firstVelocityDirection;
    public float firstVelocityMagnitude;
    public GameObject largerMass;

    public float e;

    private GameObject markerParent;
    private Vector3 first_velocity;
    private bool initialUpdate = true;
    private Vector3 i1, i2, i3;
    private Vector3 o1, o2, o3;
    private Vector3 init_position, init_velocity;
    private float p, i, w, v;
    private Vector3 h, vector_e;
    private float G, mu;
    private float next_v;
    private float E, M;
    private float k, a;
    private float next_M, next_E;
    private Vector3 next_position, next_velocity;
    private CelestialProperties lm_cp;
    private Rigidbody rb;
    private int firstTime, nextTime;

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
        G = Mathf.Pow(10,3);
    }
	
	// Update is called once per frame
	void Update () {
        PositionUpdateAlgorithm();                                                                                      //move as its orbit if collision not detected
        //if(isCollisionDetected){                                                                                      //move as its new orbit if collision detected
        //blah blah... start from beginning and sustain its collision force
        //}
	}

    void PositionUpdateAlgorithm()
    {
        if (initialUpdate)
        {
            ConvertToOrbitalElements();
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

        initialUpdate = false;
    }

    void ConvertToOrbitalElements()
    {
        //Debug.Log("Convert to OE Started");
        init_position = transform.position - largerMass.transform.position;
        init_velocity = rb.velocity;
        h = Vector3.Cross(init_position, init_velocity);
        //mu = G * lm_cp.GetMass();
        mu = G * 1;
        p = h.sqrMagnitude / mu;
        vector_e = ((init_velocity.sqrMagnitude - (mu / init_position.magnitude)) * init_position - Vector3.Dot(init_position, init_velocity) * init_velocity) / mu;
        e = vector_e.magnitude;
        a = p / (1 - Mathf.Pow(e, 2));
        i = 0;

        o1 = vector_e.normalized;
        o3 = h.normalized;
        o2 = Vector3.Cross(o3, o1);

        w = Mathf.Acos(Vector3.Dot(i1, vector_e) / e);
        if(Vector3.Dot(i2, vector_e) < 0)
        {
            w = -w;
        }
        if(vector_e == Vector3.zero)
        {
            w = 0;
        }
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
    }

    void ConvertFromOrbitalElements()
    {
        //Debug.Log("Convert From OE Started");
        next_position = (p/(1+e* Mathf.Cos(v))) * (Mathf.Cos(v) * o1 + Mathf.Sin(v) * o2);
        next_velocity = Mathf.Sqrt(p / mu) * (-Mathf.Sin(v) * o1 + (e + Mathf.Cos(v)) * o2);
    }
}
