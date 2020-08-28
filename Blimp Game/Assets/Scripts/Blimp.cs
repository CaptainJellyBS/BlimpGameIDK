using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blimp : MonoBehaviour
{
    float leftPropSpeed = 0, rightPropSpeed = 0, oxygenPercentage = 1;
    public GameObject leftPropellor, rightPropellor;
    Rigidbody rb;
    public float propRotSpeed;
    public float blimpRotSpeed, blimpMoveSpeed;
    public float maxHeight;
    public InstrumentKnob forwardSpeedIndicator, verticalSpeedIndicator, rotationIndicator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        forwardSpeedIndicator.SetValue((rb.velocity.z) / 2);
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
}
