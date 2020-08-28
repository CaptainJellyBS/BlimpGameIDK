using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blimp : MonoBehaviour
{
    float leftPropSpeed, rightPropSpeed;
    public GameObject leftPropellor, rightPropellor;
    Rigidbody rb;
    public float propRotSpeed;
    public float blimpRotSpeed, blimpMoveSpeed;
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
    }

    public void SetLeftPropellor(float strength)
    {
        leftPropSpeed = strength;
    }

    public void SetRightPropellor(float strength)
    {
        rightPropSpeed = strength;
    }
}
