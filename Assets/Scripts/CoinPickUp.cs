using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    public int coin = 1;

    AudioManager am;
    GameManager gm;

    private void Awake() {
        am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object or its parent is tagged as "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the PlayerManager component from the collided object or its parent
            //PlayerManager playerHealth = collision.gameObject.GetComponentInParent<PlayerManager>();

            am.PlaySFX(am.coin);
            gm.Bank(coin);
                Destroy(this.gameObject);
            

        }
    }
}
