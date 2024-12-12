using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackArea : MonoBehaviour
{
   public AudioManager am;
    private void Awake() {
        am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    public int swordDamage = 1; // Damage dealt by the sword

    // This method is called when the sword's collider hits another collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        // Check if the object has an EnemyHealth component (or any script that handles health)
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            am.PlaySFX(am.hit);
            // Apply damage to the enemy
            enemyHealth.TakeDamage(swordDamage);
            Debug.Log("Sword hit " + other.gameObject.name + " dealing " + swordDamage + " damage.");
        }
        Destructable objectHealth = other.GetComponent<Destructable>();

        if (objectHealth != null)
        {
            am.PlaySFX(am.hit);
            // Apply damage to the enemy
            objectHealth.TakeDamage(swordDamage);
            Debug.Log("Sword hit " + other.gameObject.name + " dealing " + swordDamage + " damage.");
        }
        SpiderBoss BossHealth = other.GetComponent<SpiderBoss>();

        if (BossHealth!= null)
        {
        am.PlaySFX(am.hit);
        // Apply damage to the enemy
        BossHealth.TakeDamage(swordDamage);
        Debug.Log("Sword hit " + other.gameObject.name + " dealing " + swordDamage + " damage.");
        }
    }
}
