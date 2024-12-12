using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebMovement : MonoBehaviour
{
    Rigidbody2D rig;
    bool grounded = false;
    Spawner spawnControl;

    private void Awake() {
        spawnControl = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
    }

    public void Start() {
        rig = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            grounded = true;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (grounded == true) {
            rig.velocity = new Vector2(-1,0) * spawnControl._enemySpeed;
        }
    }
}

