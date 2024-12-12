using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellAttack : MonoBehaviour
{
    public float maxMana = 100;
    public float currentMana;
    public float ManaCost;
    public float manaRegen = 1;
    private float regenRate;
    private float timer;
    private float chargetime = 1.5f;
    public bool canCharge = true;
    public bool charged = false;

    public Transform firePosition;
    GameObject projectile;
    GameObject chargedProjectile;
    public GameObject puff;
    public GameObject manaBarImage;
    [SerializeField] GameObject timerBar;
    [SerializeField] GameObject EquipmentSprite;

    [Header("Wands")]
    [SerializeField] GameObject level1Attack;
    [SerializeField] GameObject level2Attack;
    [SerializeField] GameObject level3Attack;
    [SerializeField] GameObject level4Attack;

    [Header("Projectiles")]
    [SerializeField] GameObject projectile1;
    [SerializeField] GameObject chargedProjectile1;
    [SerializeField] GameObject projectile2;
    [SerializeField] GameObject chargedProjectile2;
    [SerializeField] GameObject projectile3;
    [SerializeField] GameObject chargedProjectile3;
    [SerializeField] GameObject projectile4;
    [SerializeField] GameObject chargedProjectile4;

    [Header("UI")]
    public ManaBar manaBar;
    public TextMeshProUGUI manaText;
    public TimerBar chargeBar;

    AudioManager am;
    GameManager gm;
    private void Awake() {
        am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Start() 
    {
        // Initialize player's Mana and Timer when the game starts
        currentMana = maxMana;
        timer = 0f;
        chargeBar.SetMaxTime(chargetime);
        chargeBar.SetTime(timer);
        SetLevel();

        // Initialize the health bar, if it's assigned
        if (manaBar != null)
        {
            manaBar.SetMaxMana(maxMana);
            manaBar.SetMana(currentMana);
        }

        GameManager.Instance.onRestart.AddListener(ResetMana);
    }

    public void Update()
    {
        if (gm.isPlaying)
        {
            manaBarImage.SetActive(true);
            EquipmentSprite.SetActive(true);
        }
        if (gm.isPlaying == false)
        {
            manaBarImage.SetActive(false);
            EquipmentSprite.SetActive(false);
        }

        SetLevel();

        manaText.text ="" + Mathf.FloorToInt(currentMana);

        if (Input.GetMouseButton(1) && gm.isPlaying)
        {
            timerBar.SetActive(true);
            if (canCharge == true)
            {
                timer += Time.deltaTime;
                chargeBar.SetTime(timer);
            }

            if (timer >= chargetime)
            {
            currentMana -= ManaCost;
                timer = 0f;              
                charged = true;
                canCharge = false;

                currentMana = Mathf.Clamp(currentMana, 0, maxMana);

                if (manaBar != null)
                {
                    manaBar.SetMana(currentMana);
                    Debug.Log("Player Mana: " + currentMana);
                }
            }
        }
        if (Input.GetMouseButtonUp(1) && (timer > 0f))
        {
            timer = 0f;
            canCharge = true;
            chargeBar.SetTime(timer);
            timerBar.SetActive(false);
        }



        if (Input.GetMouseButtonDown(0) && gm.isPlaying)
        {
            Debug.Log("Player cast a spell");
            am.PlaySFX(am.magic);

            currentMana -= ManaCost;
            if (currentMana >= 0)
            {
                if (charged == false) {
                Instantiate(projectile, firePosition.position, firePosition.rotation);
                }
                if (charged == true) {
                Instantiate(chargedProjectile, firePosition.position, firePosition.rotation);
                charged = false;
                timerBar.SetActive(false);

                }

                currentMana = Mathf.Clamp(currentMana, 0, maxMana);

                if (manaBar != null)
                {
                    manaBar.SetMana(currentMana);
                    Debug.Log("Player Mana: " + currentMana);
                }

            timer = 0f;
            canCharge = true;
            chargeBar.SetTime(timer);

            }
            if (currentMana <= 0)
            {
                Instantiate(puff, firePosition.position, firePosition.rotation);

                currentMana = Mathf.Clamp(currentMana, 0, maxMana);

                if (manaBar != null)
                {
                    manaBar.SetMana(currentMana);
                    Debug.Log("Player Mana: " + currentMana);
                }
            }
        }

        if (currentMana < maxMana && gm.isPlaying)
        {
            currentMana += manaRegen * Time.deltaTime;
            currentMana = Mathf.Clamp(currentMana, 0, maxMana);
            manaBar.SetMana(currentMana);
        }
    }

    private void ResetMana() 
    {
        Debug.Log("ResetMana called");

        // Reset the player's health to max health
        currentMana = maxMana;

        // Update health bar if it exists
        if (manaBar != null)
        {
            manaBar.SetMana(currentMana);
        }
    }

    public void ChangeMana(float amount)
    {
        currentMana += amount;

        // Clamp the health value between 0 and maxHealth
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);

        // Update health bar if it exists
        if (manaBar != null)
        {
            manaBar.SetMana(currentMana);
        }

        // Debug to track current health
        Debug.Log("Player Mana: " + currentMana);
    }

    public void SetLevel()
    {
        if(level1Attack.activeSelf == true)
        {
            projectile = projectile1;
            chargedProjectile = chargedProjectile1;
        }

        if(level2Attack.activeSelf == true)
        {
            projectile = projectile2;
            chargedProjectile = chargedProjectile2;
        }

        if(level3Attack.activeSelf == true)
        {
            projectile = projectile3;
            chargedProjectile = chargedProjectile3;
        }

        if(level4Attack.activeSelf == true)
        {
            projectile = projectile3;
            chargedProjectile = chargedProjectile3;
        }
    }
}
