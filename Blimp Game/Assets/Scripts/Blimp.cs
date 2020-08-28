using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blimp : MonoBehaviour
{
    float leftPropSpeed = 0, rightPropSpeed = 0, oxygenPercentage = 1;
    public static Blimp Instance { get; private set; }
    public GameObject leftPropellor, rightPropellor;
    Rigidbody rb;
    public float propRotSpeed;
    public float blimpRotSpeed, blimpMoveSpeed;
    public float maxHeight;
    public float radarSweepSpeed;
    public GameObject radarSweeper, radarDot;
    public Image radarCircle;
    List<GameObject> radarDots;
    public InstrumentKnob forwardSpeedIndicator, verticalSpeedIndicator, rotationIndicator;

    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        radarDots = new List<GameObject>();
        StartCoroutine(Radar());
    }

    // Update is called once per frame
    void Update()
    {
        RotatePropellors();
        ControlBlimp();
        CalculateInstruments();
    }

    void RotatePropellors()
    {
        leftPropellor.transform.localRotation *= Quaternion.AngleAxis(propRotSpeed * Time.deltaTime * leftPropSpeed, Vector3.forward);
        rightPropellor.transform.localRotation *= Quaternion.AngleAxis(propRotSpeed * Time.deltaTime * rightPropSpeed, Vector3.forward);
    }

    void ControlBlimp()
    {
        rb.MoveRotation(transform.rotation * Quaternion.AngleAxis(blimpRotSpeed * Time.deltaTime * (leftPropSpeed - rightPropSpeed), transform.up));
        rb.AddForce(transform.forward * ((leftPropSpeed + rightPropSpeed) * blimpMoveSpeed));

        rb.AddForce(GameController.Instance.GetAirPressureForce(transform));
        rb.AddForce(Vector3.up * GameController.Instance.airPressureForce * (1-oxygenPercentage) * maxHeight);
    }

    void CalculateInstruments()
    {
        forwardSpeedIndicator.SetValue(Vector3.Project(rb.velocity, transform.forward).magnitude);
        verticalSpeedIndicator.SetValue(rb.velocity.y);
        rotationIndicator.SetValue((leftPropSpeed - rightPropSpeed)/1.5f);
    }    

    public void SetLeftPropellor(float strength)
    {
        leftPropSpeed = strength;
    }

    public void SetRightPropellor(float strength)
    {
        rightPropSpeed = strength;
    }

    public void SetOxygenPercentage(float oxygen)
    {
        oxygenPercentage = oxygen;
    }

    public void OnCollisionEnter(Collision collision)
    {
        GameController.Instance.GameOver();
    }

    IEnumerator Radar()
    {
        while (true)
        {
            float t = 0;
            float w = 1.0f / radarSweepSpeed;
            while (t < 1.0f)
            {
                t += w * Time.deltaTime;
                radarSweeper.transform.localScale = new Vector2(t, t);
                yield return null;
            }
            UpdateRadar();
        }
    }

    void UpdateRadar()
    {
        foreach(GameObject g in radarDots)
        {
            Destroy(g);
        }
        radarDots = new List<GameObject>();
        Imp[] imps = FindObjectsOfType<Imp>();
        foreach(Imp i in imps)
        {
            float d = Vector3.Distance(transform.position, i.transform.position);
            if (d > 110) { continue; }

            Vector3 relativeDirection = (i.transform.position - transform.position).normalized;
            relativeDirection = Quaternion.Euler(transform.rotation.eulerAngles.x, -transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z) * relativeDirection;
            relativeDirection = new Vector3(relativeDirection.x, relativeDirection.z, 0);

            GameObject dot = Instantiate(radarDot, radarCircle.transform);
            dot.transform.localPosition += relativeDirection * d/2;
            radarDots.Add(dot);
        }
    }
}
