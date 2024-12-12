using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {
    public static SceneController instance;

    public float universalSpeed = 1f; // The base speed value accessible to all scripts
    public float _universalSpeed = 1f;
    public float DifficultyFactor = 0.01f;

    [Header("Dash Settings")]
    public float dashSpeedMultiplier = 2f; // Multiplier for universal speed during dash
    public float dashDuration = 0.5f;      // Duration of the dash in seconds
    private bool isDashing = false;        // To track if a dash is active

    private float originalSpeed;           // Stores the original speed before dashing

    // Dictionary to track each entity and its original speed
    private Dictionary<Rigidbody2D, float> entities = new Dictionary<Rigidbody2D, float>();

    Spawner spawnControl;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        spawnControl = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
    }

    void Start() {
        //originalSpeed = universalSpeed;
    }

    void Update() {
        // Initiate a dash if the Shift key is pressed and no dash is active
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing) {
            StartCoroutine(Dash());
        }
        if (isDashing == false) {
        universalSpeed = (_universalSpeed * Mathf.Pow(spawnControl.timeAlive, DifficultyFactor));
        }
    }

    // Coroutine to handle dash logic and speed adjustments
    private IEnumerator Dash() {
        isDashing = true;
        originalSpeed = universalSpeed;

        // Apply the dash multiplier to the universal speed
        universalSpeed = originalSpeed * dashSpeedMultiplier;
        UpdateEntitySpeeds(universalSpeed);

        // Wait for the dash duration
        yield return new WaitForSeconds(dashDuration);

        // Revert back to the original speed
        universalSpeed = originalSpeed;
        UpdateEntitySpeeds(originalSpeed);

        isDashing = false;
    }

    // Method to update the speed of all registered entities based on the current universal speed
    private void UpdateEntitySpeeds(float speed) {
        List<Rigidbody2D> toRemove = new List<Rigidbody2D>();

        foreach (var entry in entities) {
            Rigidbody2D entity = entry.Key;
            float originalEntitySpeed = entry.Value;

            if (entity != null) {
                entity.velocity = Vector2.left * speed * (originalEntitySpeed / originalSpeed);
            } else {
                Debug.LogWarning("Null entity detected in UpdateEntitySpeeds. Marking for removal.");
                toRemove.Add(entity); // Collect null entities
            }
        }

        // Remove null entities
        foreach (var entity in toRemove) {
            entities.Remove(entity);
        }
    }

    // Method to register an entity with its original speed for dash effects
    public void RegisterEntity(Rigidbody2D entity, float initialSpeed) {
        if (entity == null) {
            Debug.LogWarning("Attempted to register a null entity.");
            return; // Exit early if the entity is null
        }

        if (!entities.ContainsKey(entity)) {
            entities[entity] = initialSpeed;
            Debug.Log($"Entity {entity.name} registered successfully with speed {initialSpeed}.");
        }
    }

    // Method to unregister an entity, useful when an entity is destroyed or no longer affected
    public void UnregisterEntity(Rigidbody2D entity) {
        if (entity == null) {
            Debug.LogWarning("Attempted to unregister a null entity.");
            return; // Exit early if the entity is null
        }

        if (entities.ContainsKey(entity)) {
            entities.Remove(entity);
            Debug.Log($"Entity {entity.name} unregistered successfully.");
        } else {
            Debug.LogWarning($"Entity not found in the dictionary for removal.");
        }
    }
}
