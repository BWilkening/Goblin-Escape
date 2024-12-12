using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSwordSpawner : MonoBehaviour
{
    [SerializeField] private GameObject swordPrefab; // Prefab of the Sword
    [SerializeField] private Transform GFX; // Reference to the GFX transform
    [SerializeField] private Vector3 swordOffset = new Vector3(1, 0, 0); // Offset to point the sword away from GFX
    [SerializeField] private float despawnTime = 2f; // Time after which the object despawns
    [SerializeField] private float rotationSpeed = 90f; // Rotation speed in degrees per second
    [SerializeField] private int swordDamage = 1; // Amount of damage dealt by the sword

    private GameObject spawnedSword; // To keep track of the spawned sword
    private bool isAttacking = false; // Prevent multiple spawns at once
    private Vector3 originalSwordScale;

    public AudioManager am;
    GameManager gm;
    private void Awake() {
        am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        // Store the original scale of the Sword after parenting
        if (swordPrefab != null) 
        {
            originalSwordScale = new Vector3(0.2f, 0.01f, 1.2828f);
        }
        
    }

    void Update()
    {
        // are you clicking?
        if (Input.GetMouseButtonDown(0) && !isAttacking && gm.isPlaying)
        {
            SpawnSword();
        }
    }
private void OnTriggerEnter2D(Collider2D other)
{
    // Check if the object has an EnemyHealth component
    EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

    if (enemyHealth != null)
    {
        // Apply damage to the enemy
        enemyHealth.TakeDamage(swordDamage);
        Debug.Log("Sword hit " + other.gameObject.name + " dealing " + swordDamage + " damage.");
    }
    // Check if the object has an Destructable component
    Destructable objectHealth = other.GetComponent<Destructable>();

    if (objectHealth != null)
    {
        // Apply damage to the enemy
        objectHealth.TakeDamage(swordDamage);
        Debug.Log("Sword hit " + other.gameObject.name + " dealing " + swordDamage + " damage.");
    }
    SpiderBoss BossHealth = other.GetComponent<SpiderBoss>();

    if (BossHealth!= null)
    {
        // Apply damage to the enemy
        BossHealth.TakeDamage(swordDamage);
        Debug.Log("Sword hit " + other.gameObject.name + " dealing " + swordDamage + " damage.");
    }
}
    // Spawns the sword and starts the rotation coroutine
    private void SpawnSword()
    {
        isAttacking = true; // Mark as attacking to prevent multiple spawns

        // Spawn the sword with the correct offset and rotation, so it points outward like a clock hand
        spawnedSword = Instantiate(swordPrefab, GFX.position + swordOffset, Quaternion.identity);

        // Parent the sword to the GFX so it follows properly
        spawnedSword.transform.SetParent(GFX);

        // Reset the sword's scale to its original value to prevent it from scaling oddly
        spawnedSword.transform.localScale = originalSwordScale;

        // Adjust the sword's rotation so that it points outward from GFX
        spawnedSword.transform.rotation = Quaternion.Euler(0, 0, 90); // Adjust to face outward properly

        // Add a collider and damage component to the sword
        AddCollisionHandlingToSword(spawnedSword);

        am.PlaySFX(am.miss);

        StartCoroutine(RotateAndDespawn());
    }

    // Coroutine that rotates the sword around the GFX and despawns it
    private IEnumerator RotateAndDespawn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < despawnTime)
        {
            // Rotate the sword around the GFX's center (pivot point is GFX's position) in the clockwise direction
            spawnedSword.transform.RotateAround(GFX.position, Vector3.forward, -rotationSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        // Despawn the sword after rotation
        Destroy(spawnedSword);
        isAttacking = false; // Reset attack status to allow another attack
    }

    // Adds a collider and collision handling logic to the sword
    private void AddCollisionHandlingToSword(GameObject sword)
    {
        // Ensure the sword has a Collider2D
        Collider2D swordCollider = sword.GetComponent<Collider2D>();
        if (swordCollider == null)
        {
            swordCollider = sword.AddComponent<BoxCollider2D>(); // Add a BoxCollider2D if one doesn't exist
            swordCollider.isTrigger = true; // Make sure it's a trigger collider
        }

        // Add the SwordDamageHandler component to handle collision and damage dealing
        SwordDamageHandler damageHandler = sword.AddComponent<SwordDamageHandler>();
        damageHandler.swordDamage = swordDamage; // Set the damage value for the sword
    }
}

// Separate class to handle collision detection and damage dealing for the sword
public class SwordDamageHandler : MonoBehaviour
{

    public AudioManager am;
    private void Awake() {
        am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    public int swordDamage = 1; // Damage dealt by the sword

    // This method is called when the sword's collider hits another collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        // Check if the object has an EnemyHealth component (or any script that handles health)
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            am.PlaySFX(am.hit);
            // Apply damage to the enemy
            enemyHealth.TakeDamage(swordDamage);
            Debug.Log("Sword hit " + other.gameObject.name + " dealing " + swordDamage + " damage.");
        }
        Destructable objectHealth = other.GetComponent<Destructable>();

        if (objectHealth != null)
        {
            am.PlaySFX(am.hit);
            // Apply damage to the enemy
            objectHealth.TakeDamage(swordDamage);
            Debug.Log("Sword hit " + other.gameObject.name + " dealing " + swordDamage + " damage.");
        }
        SpiderBoss BossHealth = other.GetComponent<SpiderBoss>();

        if (BossHealth!= null)
        {
        am.PlaySFX(am.hit);
        // Apply damage to the enemy
        BossHealth.TakeDamage(swordDamage);
        Debug.Log("Sword hit " + other.gameObject.name + " dealing " + swordDamage + " damage.");
        }
    }
}
