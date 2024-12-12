using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random=UnityEngine.Random;

public class SpiderBoss : MonoBehaviour
{

    public GameObject SpiderLeg;
    public HealthBar healthBar;

    [SerializeField] private Transform barrel;
    [SerializeField] private Rigidbody2D web;
    
    private float webSpeed;
    private float randomDirectionModifier, randomSpeed;
    private float actionCooldown;
    private float elapsedTime = 0f;

    public float startActionCooldown;
    public float _speed = 3;
    public float moveDuration = 1f;
    public int maxHealth;
    public int currentHealth;
    public int damage = 1;
    private float rand;

    //[SerializeField] private Transform objectParent;
    private SpriteRenderer spriteR;
    Transform _target;
    Vector3 _origin;
    public bool isActing;
    private bool choice;

    AudioManager am;

    Spawner spawner;

    public void Awake() {
        am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        findTarget();
        spriteR = gameObject.GetComponentInChildren<SpriteRenderer>();
    }
    void Start()
    {
        am.SpiderMusic();
        actionCooldown = startActionCooldown;
        _origin = transform.position;
        isActing = false;
        choice = true;
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(currentHealth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        actionCooldown -= Time.deltaTime;
        if(actionCooldown <= 0)
        {
            if (isActing == false) {
                    choice = true;
            }
            else {
                choice = false;
            }

            if (choice == true) {
            RandomChoice();
            isActing = true;
            }

            if (rand == 0) {
            Shoot();
            }

            if (rand == 1) {
            Charge();
            }
            if (rand == 2) {
            LegAttack();
            }
        }
        if (isActing == false) {
            Retreat();
        }
    }

    private void RandomChoice() {
        rand = Random.Range(0, 3);
        Debug.Log("Random choice " + rand);
    }

    public void findTarget() {
        GameObject temp = GameObject.FindGameObjectWithTag("Player");
        if (temp != null) {
            _target = temp.transform;
        }
        else {
            Debug.Log("Player not Found");
        }
    }
    public void Shoot() {
        
        for (int i = 0; i < 5; i++) {
            var newWeb = Instantiate(web, barrel.position, barrel.rotation);
            randomDirectionModifier = Random.Range(-0.5f, 0.5f);
            randomSpeed = Random.Range(100f, 1000f);
            newWeb.AddRelativeForce(new Vector2 (0f + randomDirectionModifier, 1f) * randomSpeed);
            //newWeb.transform.parent = objectParent.transform;
        }
            isActing = false;
            actionCooldown = startActionCooldown;
    }

    public void Charge() {
        findTarget();
        elapsedTime += Time.deltaTime;
        float progress = elapsedTime / moveDuration;
        transform.position = Vector2.Lerp(transform.position, _target.position, (progress));
        Debug.Log("Boss is Charging");
    }
    public void LegAttack() {
        StartCoroutine(Pattern());

        isActing = false;
        actionCooldown = (startActionCooldown +5);
    }

    public void Retreat() {
        transform.position = Vector2.MoveTowards(transform.position, _origin, _speed * Time.deltaTime);
        Debug.Log("Boss is Retreating");
    }
    //When touching the player damages and ends the attack movement
     private void OnCollisionEnter2D(Collision2D collision)
    {        
        // Check if the collided object or its parent is tagged as "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the PlayerManager component from the collided object or its parent
            PlayerManager playerHealth = collision.gameObject.GetComponentInParent<PlayerManager>();

            // Check if the PlayerManager component exists
            if (playerHealth != null)
            {
                playerHealth.HurtHealth(-damage); // Apply damage to the player's health
            }
            else
            {
                Debug.LogError("PlayerManager component is missing on the Player.");
            }
            isActing = false;
            elapsedTime = 0f;
            actionCooldown = startActionCooldown;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        am.PlaySFX(am.hiss);

        if (currentHealth > 0) {
            isActing = false;
            elapsedTime = 0f;
            actionCooldown = startActionCooldown;
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
        Debug.Log("Boss Health: " + currentHealth);

        // Check if health reaches 0
        if (currentHealth <= 0)
        {
            Debug.Log("Boss Health reached 0.");
            am.TownMusic();
            spawner.spiderBossIsKilled = true;
            Destroy(gameObject);
        }
    }

    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private IEnumerator Invulnerability() {
        Physics2D.IgnoreLayerCollision(14, 13, true);
            for (int i = 0; i < numberOfFlashes; i++)
            {
                spriteR.color = new Color(1,0,0,0.5f);
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
                spriteR.color = Color.white;
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            } 
        Physics2D.IgnoreLayerCollision(14, 13, false);
    }
    private IEnumerator Pattern() {
        Vector3 pos1 = new Vector3(10, -4, -1);
        Vector3 pos2 = new Vector3(10, -3, -1);
        Vector3 pos3 = new Vector3(10, -2, -1);
        Vector3 pos4 = new Vector3(10, -1, -1);

        int random = Random.Range(0, 5);

        if (random == 0) {
        Instantiate(SpiderLeg, pos1, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(SpiderLeg, pos2, Quaternion.identity);
        }
        if (random == 1) {
        Instantiate(SpiderLeg, pos4, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(SpiderLeg, pos1, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(SpiderLeg, pos2, Quaternion.identity);
        }
        if (random == 2) {
        Instantiate(SpiderLeg, pos1, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(SpiderLeg, pos2, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(SpiderLeg, pos3, Quaternion.identity);
        }
        if (random == 3) {
        Instantiate(SpiderLeg, pos1, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(SpiderLeg, pos2, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(SpiderLeg, pos3, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(SpiderLeg, pos4, Quaternion.identity);
        }
        if (random == 4) {
        Instantiate(SpiderLeg, pos4, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(SpiderLeg, pos3, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(SpiderLeg, pos2, Quaternion.identity);
        }
            
    }
    
}
