using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int healing = 1;
    public float stam = 30;
    public float mana = 10;

    AudioManager am;

    private void Awake() {
        am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object or its parent is tagged as "Player"
        if (collision.gameObject.CompareTag("Player"))// || collision.gameObject.transform.parent.CompareTag("Player"))
        {
            // Get the PlayerManager component from the collided object or its parent
            PlayerManager playerHealth = collision.gameObject.GetComponentInParent<PlayerManager>();

            // Check if the PlayerManager component exists
            if (playerHealth != null)
            {
                am.PlaySFX(am.heal);
                playerHealth.HealHealth(healing); // Apply healing to the player's health
                playerHealth.ChangeStamina(stam);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.LogError("PlayerManager component is missing on the Player.");
            }
            SpellAttack spellAttack = collision.gameObject.GetComponent<SpellAttack>();

            if (spellAttack != null)
            {
                spellAttack.ChangeMana(mana);
            }
        }
    }

}
