using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public int maxHealth = 1; // Max health of the enemy
    private int currentHealth;

    AudioManager am;

    private void Awake() {
        am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health
        //GetComponent<Rigidbody2D>();

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce current health by the damage amount

        Debug.Log("Destructable took damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); // Trigger Die method when health reaches 0 or below
            
        }
    }

    private void Die()
    {
        // Debug to check if this method is called
        Debug.Log("Destroyed.");

        // Destroy the enemy GameObject
        Destroy(this.gameObject);
    }
}

