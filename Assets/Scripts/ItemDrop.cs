using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{

    Rigidbody2D rig;
    float magnitude = 5;

    bool grounded = false;

    Spawner spawnControl;
    BaseEntityMovement baseEntityMovement;

    private void Awake() {
        spawnControl = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        baseEntityMovement = gameObject.GetComponent<BaseEntityMovement>();
    }

    public void Start() {
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = new Vector2(1,1) * magnitude;
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
            //rig.velocity = new Vector2(-1,0) * spawnControl._enemySpeed;
            baseEntityMovement.enabled = true;
        }
    }
}
