using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        CircleCollider2D web = this.gameObject.GetComponent<CircleCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the object has a PlayerContoller component
        PlayerController playerController = other.gameObject.GetComponentInParent<PlayerController>();

        if (playerController != null)
        {
            // Apply damage to the enemy
            playerController.GetWebbed();
        }
        else {
            Debug.Log("The Web Didn't work.");
        }
    }
}
