﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //Mouselook stolen from: https://answers.unity.com/questions/29741/mouse-look-script.html
    #region mouselook stuff
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationX = 0F;
    float rotationY = 0F;
    Quaternion originalRotation;
    #endregion

    public GameObject bullet;
    public Transform leftOut, rightOut;
    public float fireRate;


    Camera c;
    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.localRotation;
        StartCoroutine(Shooting());
    }

    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            // Read the mouse input axis
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            transform.localRotation = originalRotation * yQuaternion;
        }
    }

    private void OnEnable()
    {
        c = GetComponentInChildren<Camera>(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        c.enabled = true;

        StartCoroutine(Shooting());
    }

    private void OnDisable()
    {
        c = GetComponentInChildren<Camera>(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        c.enabled = false;
    }

    void Shoot(int barrel)
    {
        switch (barrel)
        {
            case 0:
                Instantiate(bullet, leftOut.position, transform.rotation);
                break;
            case 1:
                Instantiate(bullet, rightOut.position, transform.rotation);
                break;
            default:
                Instantiate(bullet, rightOut.position, transform.rotation);
                break;
        }
    }

    IEnumerator Shooting()
    {
        int barrel = 0;
        while(enabled)
        {
            if(Input.GetMouseButton(0))
            {
                GameController.Instance.hasShotTurret = true;
                Shoot(barrel);
                barrel++; barrel %= 2;
                yield return new WaitForSeconds(fireRate);
            }
            else
            {
                yield return null;
            }
        }
    }


    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
         angle += 360F;
        if (angle > 360F)
         angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
