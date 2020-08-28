using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public Camera thirdperson, firstPerson;
    public Turret frontTurret, rearTurret;
    public float airPressureForce;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FirstPersonMode();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ThirdPersonMode();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            FrontTurretMode();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            RearTurretMode();
        }
    }

    #region camera switching
    public void FirstPersonMode()
    {
        thirdperson.enabled = false; frontTurret.enabled = false; rearTurret.enabled = false; firstPerson.enabled = true;
    }

    public void ThirdPersonMode()
    {
        thirdperson.enabled = true; frontTurret.enabled = false; rearTurret.enabled = false; firstPerson.enabled = false;
    }

    public void FrontTurretMode()
    {
        thirdperson.enabled = false; frontTurret.enabled = true; rearTurret.enabled = false; firstPerson.enabled = false;
    }

    public void RearTurretMode()
    {
        thirdperson.enabled = false; frontTurret.enabled = false; rearTurret.enabled = true; firstPerson.enabled = false;
    }
    #endregion

    public Vector3 GetAirPressureForce(Transform obj)
    {
        return Vector3.down * airPressureForce * obj.position.y;
    }
}
