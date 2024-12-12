using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag ("Obstacle")) {
            Destroy(other.gameObject);
            //GameManager Set Game Over
        }
        if (other.gameObject.CompareTag ("Weapon")) {
            Destroy(other.gameObject);
            //GameManager Set Game Over
        }
}
}

