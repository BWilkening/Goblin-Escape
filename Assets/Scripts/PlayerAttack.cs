using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
     GameObject attackArea = default;

     [SerializeField] GameObject level1Attack;
     [SerializeField] GameObject level2Attack;
     [SerializeField] GameObject level3Attack;
     [SerializeField] GameObject level4Attack;

    private bool attacking = false;

    private float timetoAttack = 0.12f;
    private float timer = 0f;

    public AudioManager am;
    public GameManager gm;
    private void Awake() {
        am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && gm.isPlaying)
        {
            am.PlaySFX(am.miss);
            Attack();
        }
        if(attacking)
        {
            timer += Time.deltaTime;

            if(timer >= timetoAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }
    }

    private void Attack(){
        attacking = true;
        attackArea.SetActive(attacking);
    }

    public void SetLevel1() {
        level1Attack.SetActive(true);
        level2Attack.SetActive(false);
        level3Attack.SetActive(false);
        level4Attack.SetActive(false);
    }
    public void SetLevel2() {
        level1Attack.SetActive(false);
        level2Attack.SetActive(true);
        level3Attack.SetActive(false);
        level4Attack.SetActive(false);
    }

    public void SetLevel3() {
        level1Attack.SetActive(false);
        level2Attack.SetActive(false);
        level3Attack.SetActive(true);
        level4Attack.SetActive(false);
    }

    public void SetLevel4() {
        level1Attack.SetActive(false);
        level2Attack.SetActive(false);
        level3Attack.SetActive(false);
        level4Attack.SetActive(true);
    }
}
