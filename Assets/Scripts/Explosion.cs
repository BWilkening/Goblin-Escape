using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int spellDamage = 1;
    private float animationTime = 0.38f;
    private float timer = 0f;

    AudioManager am;
    private void Awake() {
        am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start() {
        am.PlaySFX(am.explosion);
    }

    public void Update() {
        timer += Time.deltaTime;

            if(timer >= animationTime)
            {
                Destroy(gameObject);
            }    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        // Check if the object has an EnemyHealth component (or any script that handles health)
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            //am.PlaySFX(am.hit);
            // Apply damage to the enemy
            enemyHealth.TakeDamage(spellDamage);
            Debug.Log("Spell hit " + other.gameObject.name + " dealing " + spellDamage + " damage.");
        }
        Destructable objectHealth = other.GetComponent<Destructable>();

        if (objectHealth != null)
        {
            //am.PlaySFX(am.hit);
            // Apply damage to the enemy
            objectHealth.TakeDamage(spellDamage);
            Debug.Log("Spell hit " + other.gameObject.name + " dealing " + spellDamage + " damage.");
        }
        SpiderBoss BossHealth = other.GetComponent<SpiderBoss>();

        if (BossHealth!= null)
        {
        //am.PlaySFX(am.hit);
        // Apply damage to the enemy
        BossHealth.TakeDamage(spellDamage);
        Debug.Log("Spell hit " + other.gameObject.name + " dealing " + spellDamage + " damage.");
        }
    }
}
