using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReactiveEnemy : MonoBehaviour
{
    public bool attacking = false;
    public bool canAttack = true;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canAttack == true)
        {
            attacking = true;
            animator.SetBool("attacking", true);
            canAttack = false;
        }
    }
}
