using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Camera thirdperson, firstPerson;
    bool inThirdPerson;
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
}
