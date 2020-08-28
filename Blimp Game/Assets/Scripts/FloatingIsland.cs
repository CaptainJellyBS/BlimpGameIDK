using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingIsland : MonoBehaviour
{
    public float minHeight, maxHeight;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, Random.Range(minHeight, maxHeight), transform.position.z);
    }
    
}
