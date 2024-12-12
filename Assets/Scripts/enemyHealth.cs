using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; // Max health of the enemy
    private int currentHealth;

    AudioManager am;
    ItemSpawn itemSpawn;
    FoodSpawn foodSpawn;

    private void Awake() {
        am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        itemSpawn = gameObject.GetComponentInChildren<ItemSpawn>();
        foodSpawn = gameObject.GetComponentInChildren<FoodSpawn>();
    }

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health
        //GetComponent<Rigidbody2D>();

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce current health by the damage amount

        Debug.Log("Enemy took damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            itemSpawn.SpawnCoin();
            foodSpawn.SpawnFood();
            Die(); // Trigger Die method when health reaches 0 or below
            
        }
    }

    private void Die()
    {
        // Debug to check if this method is called
        Debug.Log("Enemy has died.");

        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}
