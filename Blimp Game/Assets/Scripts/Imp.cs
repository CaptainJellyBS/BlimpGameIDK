using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Imp : MonoBehaviour
{
    public float fireRate, moveSpeed, fireDistance, bobTimeMin, bobTimeMax, bobSpeed;
    int direction;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
        StartCoroutine(Bob());
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Blimp.Instance.transform.position - transform.position);
                
        if(Vector3.Distance(transform.position, Blimp.Instance.transform.position) >= fireDistance-5)
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        }

        transform.position += Vector3.up * direction * bobSpeed * Time.deltaTime;
    }

    IEnumerator Shoot()
    {
        while(true)
        {
            yield return new WaitForSeconds(fireRate);
            if(Vector3.Distance(transform.position, Blimp.Instance.transform.position) < fireDistance)
            {
                Instantiate(projectile, transform.position + transform.forward, Quaternion.LookRotation(Blimp.Instance.transform.position - transform.position));
            }
        }
    }

    IEnumerator Bob()
    {
        direction = 1;
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(bobTimeMin, bobTimeMax));
            direction *= -1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
