using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

     PlayerManager playerHealth;
    public int damage = 1;


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            //if (playerHealth != null) {
                playerHealth = collision.gameObject.GetComponent<PlayerManager>();
            //}
            //PlayerManager.playerHealth.TakeDamage(damage);
        }
    }
}
