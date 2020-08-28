using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentKnob : MonoBehaviour
{
    public GameObject pointer;
    float value;

    public void SetValue(float val)
    {
        value = val;
        pointer.transform.rotation = Quaternion.AngleAxis((value * -120), Vector3.forward);
    }

}
