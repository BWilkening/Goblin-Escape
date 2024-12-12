using System.Collections;
using UnityEngine;

public class EnemySpeed : MonoBehaviour
{
    public float impulseForce = 5f; // The strength of the impulse force
    private Rigidbody2D rb;
    private float forceMultiplier;
    public float slowdownDuration = 1f; // How long it takes to slow down after the dash

    private bool isSlowingDown = false; // To track if the slowing down process is happening

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Check if the SceneController instance exists and get the dash multiplier
        if (SceneController.instance != null)
        {
            forceMultiplier = SceneController.instance.dashSpeedMultiplier;
        }
        else
        {
            Debug.LogError("SceneController instance is not set or initialized!");
        }
    }

    void Update()
    {
        // Check if the left shift key is pressed and we're not already dashing or slowing down
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSlowingDown)
        {
            ApplyImpulse();
        }
    }

    // Function to apply an impulse in the -x direction
    void ApplyImpulse()
    {
        Vector2 forceDirection = new Vector2(-1, 0); // -x direction
        rb.AddForce(forceDirection * impulseForce * forceMultiplier, ForceMode2D.Impulse);

        // Start the slowdown coroutine after applying the impulse
        StartCoroutine(SlowDownToNormalSpeed());
    }

    // Coroutine to gradually slow down the player after the dash
    private IEnumerator SlowDownToNormalSpeed()
    {
        isSlowingDown = true; // Flag to prevent other dashes during slowdown

        float elapsedTime = 0f;
        Vector2 initialVelocity = rb.velocity; // Store the current velocity after the dash
        Vector2 targetVelocity = new Vector2(0, rb.velocity.y); // Target velocity to gradually reduce to (just stop in the x-axis)

        while (elapsedTime < slowdownDuration)
        {
            elapsedTime += Time.deltaTime;
            // Gradually interpolate between the initial velocity and the target velocity
            rb.velocity = Vector2.Lerp(initialVelocity, targetVelocity, elapsedTime / slowdownDuration);
            yield return null;
        }

        // Ensure the velocity is exactly the target velocity at the end
        rb.velocity = targetVelocity;

        isSlowingDown = false; // Allow dashing again
    }
}
