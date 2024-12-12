using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float projectileSpeed;
    public GameObject explosion;
    public GameObject miniExplosion;
    public Transform explosionPosition;

    public bool Charged = false;

    private Rigidbody2D rb;

    AudioSource AS;
    AudioManager am;

    private void Awake() {
        AS = GetComponent<AudioSource>();
        am = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right *projectileSpeed;
        AS.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Charged == true){
            Instantiate(explosion, explosionPosition.position, Quaternion.identity);
        }
        if (Charged == false){
            Instantiate(miniExplosion, explosionPosition.position, Quaternion.identity);
        }
        AS.Stop();
        Destroy(gameObject);
    }
}
