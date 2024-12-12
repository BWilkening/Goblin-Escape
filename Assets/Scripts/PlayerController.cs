using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform GFX; // Reference to the GFX object
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float jumpTime = 0.3f;
    [SerializeField] private float crouchHeight = 6f;
    [SerializeField] private float standHeight = 12f;
    [SerializeField] private float maxVerticalVelocity = 10f; // Maximum vertical velocity clamp
    [SerializeField] private float maxHeight = 5f; // Maximum Y-axis height clamp for GFX
    [SerializeField] private float StatusDuration;
    [SerializeField] private SpriteRenderer Web;

    public ParticleSystem dust;

    private bool isGrounded = false;
    private bool isJumping = false;
    public bool isWebbed = false;
    private float jumpTimer;

    private Vector3 initialPosition;
    private Vector3 initialGFXScale;

    public GameManager gm;
    public AudioManager am;
    Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        // Store the initial position and scale
        initialPosition = transform.position;
        initialGFXScale = GFX.localScale;

        // Register the ResetPlayer method with GameManager's onRestart event
        GameManager.Instance.onRestart.AddListener(ResetPlayer);
    }

    private void Update()
    {
        if (gm.isPlaying == true) {
        animator.SetBool("isPlaying", true);

        }
        if(gm.isPlaying == false) {
            animator.SetBool("isPlaying", false);
        }

        if (isWebbed == false) {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);
        }
        if (isWebbed == true) {
            isGrounded = false;
        }

        #region JUMPING
        // Spacebar for jumping
        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && gm.isPlaying)
        {
            //isGrounded = false;
            animator.SetBool("isJumping", true);
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            CreateDust();
            am.PlaySFX(am.jump);
        }

        if (isJumping && Input.GetKey(KeyCode.Space))
        {

            if (jumpTimer < jumpTime)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            jumpTimer = 0;
        }
        #endregion

        #region CROUCHING
        // Left Control for crouching
        if (isGrounded && Input.GetKey(KeyCode.LeftControl) && gm.isPlaying)
        {
            GFX.localScale = new Vector3(GFX.localScale.x, crouchHeight, GFX.localScale.z);
        }

        if (isJumping)
        {
            GFX.localScale = new Vector3(GFX.localScale.x, standHeight, GFX.localScale.z);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            GFX.localScale = new Vector3(GFX.localScale.x, standHeight, GFX.localScale.z);
        }
        #endregion

        // Clamp the GFX Y position (height)
        ClampGFXHeight();
    }

    private void FixedUpdate()
    {
        // Clamp the player's vertical velocity to avoid launching too fast
        ClampVerticalVelocity();
        animator.SetFloat("yVelocity", rb.velocity.y);
    }
    

    private void ClampVerticalVelocity()
    {
        // Ensure the player's vertical velocity doesn't exceed the max allowed value
        if (rb.velocity.y > maxVerticalVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxVerticalVelocity);
        }
        else if (rb.velocity.y < -maxVerticalVelocity)
        {
            rb.velocity = new Vector2(rb.velocity.x, -maxVerticalVelocity);
        }
    }

    private void ClampGFXHeight()
    {
        // Clamp the GFX Y position to prevent it from going above the max height
        if (GFX.position.y > maxHeight)
        {
            GFX.position = new Vector3(GFX.position.x, maxHeight, GFX.position.z);
        }
    }

    
    private void ResetPlayer()
    {
        Debug.Log("ResetPlayer called");

        // Reset the player's position and scale
        transform.position = initialPosition;
        GFX.localScale = initialGFXScale;

        // Reset other variables
        isJumping = false;
        jumpTimer = 0;

        // Reset Rigidbody and enable player controls
        rb.velocity = Vector2.zero;
    }
    //triggers end of jumping animation
    private void OnTriggerEnter2D(Collider2D Collision) {
          //isGrounded = true;
            animator.SetBool("isJumping", false);
            CreateDust();
        
    }

    private void CreateDust() {
        dust.Play();
    }

    public void GetWebbed() {
        StartCoroutine(Webbed());
    }

    private IEnumerator Webbed() {
        isWebbed = true;
        Web.enabled = true;
        Debug.Log("The Player is Webbed.");
        yield return new WaitForSeconds(StatusDuration);
        isWebbed = false;
        Web.enabled = false;
    }
    
}
