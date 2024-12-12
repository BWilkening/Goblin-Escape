using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object or its parent is tagged as "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the PlayerManager component from the collided object or its parent
            PlayerManager playerHealth = collision.gameObject.GetComponentInParent<PlayerManager>();

            // Check if the PlayerManager component exists
            if (playerHealth != null)
            {
                playerHealth.HurtHealth(-damage); // Apply damage to the player's health
            }
            else
            {
                Debug.LogError("PlayerManager component is missing on the Player.");
            }
        }
    }
}
