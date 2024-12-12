using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxHealth = 4;
    public float maxStamina = 60;
    public int currentHealth;
    public float currentStamina;
    public HealthBar healthBar;
    public TextMeshProUGUI healthText;
    public StaminaBar staminaBar;
    public TextMeshProUGUI staminaText;
    public GameManager gameManager;
    public AudioManager am;

    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteR;

    private void Awake() {
            spriteR = gameObject.GetComponent<SpriteRenderer>();
    }
    

    private void Start() 
    {

        // Initialize player's health and stamina when the game starts
        currentHealth = maxHealth;
        currentStamina = maxStamina;

        // Initialize the health bar, if it's assigned
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(currentHealth);

            staminaBar.SetMaxStamina(maxStamina);
            staminaBar.SetStamina(currentStamina);
        }

        // Register the ResetHealth method with GameManager's onRestart event
        GameManager.Instance.onRestart.AddListener(ResetHealth);
        GameManager.Instance.onRestart.AddListener(ResetStamina);

    }

    private void Update()
    {
        //drains stamina by rate of time while game is playing
        if(currentStamina > 0 && gameManager.isPlaying == true) {
            currentStamina -= Time.deltaTime;
            staminaBar.SetStamina(currentStamina);
        } 
        //ends the game if stamina reaches 0
        else if (currentStamina <= 0)
        {
            Debug.Log("Player Stamina reached 0, triggering Game Over.");
            GameManager.Instance.GameOver(); // Call GameOver
        
        }

        healthText.text ="" + Mathf.FloorToInt(currentHealth);

        staminaText.text ="" + Mathf.FloorToInt(currentStamina);
    }

    public void ChangeStamina(float amount)
    {
        currentStamina += amount;

        // Clamp the health value between 0 and maxHealth
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        // Update health bar if it exists
        if (staminaBar != null)
        {
            staminaBar.SetStamina(currentStamina);
        }

        // Debug to track current health
        Debug.Log("Player Stamina: " + currentStamina);
        
    }

    public void HealHealth(int amount){
        currentHealth += amount;

        // Clamp the health value between 0 and maxHealth
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update health bar if it exists
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        // Debug to track current health
        Debug.Log("Player Health: " + currentHealth);
    }

    public void HurtHealth(int amount)
    {
        currentHealth += amount;

        am.PlaySFX(am.hurt);

        if (currentHealth > 0) {
            StartCoroutine(Invulnerability());
        }

        // Clamp the health value between 0 and maxHealth
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update health bar if it exists
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        // Debug to track current health
        Debug.Log("Player Health: " + currentHealth);

        // Check if health reaches 0
        if (currentHealth <= 0)
        {
            Debug.Log("Player Health reached 0, triggering Game Over.");
            //gameObject.SetActive(false);
            GameManager.Instance.GameOver(); // Call GameOver
        }
    }

    private void ResetHealth()
    {
        Debug.Log("ResetHealth called in PlayerManager");

        // Reset the player's health to max health
        currentHealth = maxHealth;

        // Update health bar if it exists
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
    }

    private void ResetStamina() 
    {
        Debug.Log("ResetStamina called in PlayerManager");

        // Reset the player's health to max health
        currentStamina = maxStamina;

        // Update health bar if it exists
        if (staminaBar != null)
        {
            staminaBar.SetStamina(currentStamina);
        }
    }

    private IEnumerator Invulnerability() {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        Physics2D.IgnoreLayerCollision(9, 14, true);
        Physics2D.IgnoreLayerCollision(9, 15, true);
            for (int i = 0; i < numberOfFlashes; i++)
            {
                spriteR.color = new Color(1,0,0,0.5f);
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
                spriteR.color = Color.white;
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            } 
        Physics2D.IgnoreLayerCollision(9, 10, false);
        Physics2D.IgnoreLayerCollision(9, 14, false);
        Physics2D.IgnoreLayerCollision(9, 15, false);

    }
}
