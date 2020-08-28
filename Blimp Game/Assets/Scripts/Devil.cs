using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : MonoBehaviour
{
    public float timeBetweenImpSpawnFar, timeBetweenImpSpawnClose, impTimeVar, timeBetweenFireball, fireballTimeVar;
    public float playerCloseDistance;
    public int maxHP, impsOnDeath;
    private int currentHP;
    public float impDist;
    public GameObject imp, fireball;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnImps());
        StartCoroutine(ShootFireball());
        currentHP = maxHP;
    }

    IEnumerator SpawnImps()
    {
        while(true)
        {
            bool playerClose = Vector3.Distance(transform.position, Blimp.Instance.transform.position) <= playerCloseDistance;
            if (playerClose)
            {
                yield return new WaitForSeconds(Random.Range(timeBetweenImpSpawnClose - impTimeVar, timeBetweenImpSpawnClose + impTimeVar));
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(timeBetweenImpSpawnFar - impTimeVar, timeBetweenImpSpawnFar + impTimeVar));
            }

            SpawnImp();
        }
    }

    void SpawnImp()
    {
        Vector3 offset = new Vector3(Random.Range(-impDist, impDist), 0, Random.Range(-impDist, impDist));
        Instantiate(imp, transform.position + offset, Quaternion.identity);
    }

    IEnumerator ShootFireball()
    {
        bool playerClose = Vector3.Distance(transform.position, Blimp.Instance.transform.position) <= playerCloseDistance*2;
        if (playerClose)
        {
            yield return new WaitForSeconds(Random.Range(timeBetweenFireball - fireballTimeVar, timeBetweenFireball + fireballTimeVar));
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(fireball, transform.position + (3*transform.forward), Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.CompareTag("Bullet"))
        {
            currentHP--;
            if (currentHP <= 0) 
            {
                for (int i = 0; i < impsOnDeath; i++)
                {
                    SpawnImp();
                }
                Destroy(gameObject); 
                GameController.Instance.CountDevils(); 
            }
        }
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Blimp.Instance.transform.position - transform.position);
    }
}
