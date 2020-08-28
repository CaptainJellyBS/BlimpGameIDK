using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public Camera thirdperson, firstPerson;
    public float airPressureForce;
    bool inThirdPerson;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        inThirdPerson = thirdperson.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) { SwitchCameras(); }
    }

    void SwitchCameras()
    {
        thirdperson.enabled = !inThirdPerson;
        firstPerson.enabled = inThirdPerson;
        inThirdPerson = thirdperson.enabled;
    }

    public Vector3 GetAirPressureForce(Transform obj)
    {
        return Vector3.down * airPressureForce * obj.position.y;
    }
}
