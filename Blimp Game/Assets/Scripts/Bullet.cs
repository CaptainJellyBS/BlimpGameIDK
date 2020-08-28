using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public bool isFireball;
    protected void Start()
    {
        StartCoroutine(Kill());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    protected void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
