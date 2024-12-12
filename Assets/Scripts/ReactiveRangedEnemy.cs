using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveRangedEnemy : MonoBehaviour
{
    Spawner spawner;
    public GameObject projectile;
    public Transform Barrel;

    // Start is called before the first frame update
    void Start()
    {
    spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        GameObject projectileToSpawn = projectile;

        GameObject spawnedProjectile = Instantiate(projectileToSpawn, Barrel.position, Quaternion.identity);
        //spawnedProjectile.transform.parent = spawner.objectParent;
        yield return new WaitForSeconds(2);
    }
}
