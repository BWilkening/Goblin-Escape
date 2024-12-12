using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLeg : MonoBehaviour
{
    private SpriteRenderer spriteR;
    public float growTimer = 0f;
    public float shrinkTimer = 0f;
    public float growTime = 2f;
    public float MaxSize = 4f;
    public float OrigScale = 1f;
    //private float flashDuration = 2f;
    //private int numberOfFlashes = 3;
    public bool isAttacking = true;



    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponentInChildren<SpriteRenderer>();
        isAttacking = true;
    }

    public void Update() {
        if (isAttacking == true) {
            StartCoroutine(Stretch());
        }

        if (isAttacking == false) {
            Destroy(gameObject);
        }
    }
    private IEnumerator Stretch() {
        Vector2 startScale = transform.localScale;
        Vector2 maxScale = new Vector2(MaxSize, OrigScale);

        // for (int i = 0; i < numberOfFlashes; i++)
        //     {
        //         spriteR.color = new Color(1, 0, 0, 0.1f);
        //         yield return new WaitForSeconds(flashDuration / (numberOfFlashes * 2));
        //         spriteR.color = Color.white;
        //         yield return new WaitForSeconds(flashDuration / (numberOfFlashes * 2));
        //     }

        yield return new WaitForSeconds(1f);
        do {
            transform.localScale = Vector3.Lerp(startScale, maxScale, growTimer/growTime);
            growTimer += Time.deltaTime;
            Debug.Log("Leg attacking");
            yield return null;
        }
        while (growTimer < growTime);

        Destroy(GetComponentInChildren<Enemy_Combat>());
        
        yield return new WaitForSeconds(2f);
        do {
            Destroy(GetComponentInChildren<Enemy_Combat>());
            spriteR.color = new Color(1,1,1,0.5f);
            transform.localScale = Vector3.Lerp(maxScale, startScale, shrinkTimer/growTime);
            shrinkTimer += Time.deltaTime;
            Debug.Log("Leg shrinking");
            yield return null;
        }
        while (shrinkTimer < growTime);

        isAttacking = false;
    }
}
