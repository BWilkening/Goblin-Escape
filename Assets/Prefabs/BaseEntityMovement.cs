using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntityMovement : MonoBehaviour
{
    Rigidbody2D rig;

    Spawner spawnControl;

    private void Awake() {
        spawnControl = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        // Register this entity with its base speed
        SceneController.instance.RegisterEntity(rig, spawnControl._enemySpeed);
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = Vector2.left * SceneController.instance.universalSpeed * spawnControl._enemySpeed;
        //rig.velocity = new Vector2(-1,0) * spawnControl._enemySpeed;
    }
    private void OnDestroy()
    {
        // Unregister the entity when it’s destroyed
        SceneController.instance.UnregisterEntity(rig);
    }
}

    