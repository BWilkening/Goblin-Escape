using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;

    ItemDrop itemDrop;
    
    private void Awake() {
        itemDrop = gameObject.GetComponent<ItemDrop>();
    }

    public void SpawnFood() {
        float randomNumber = Random.Range(0, 100);
        Debug.Log(randomNumber);

         if (randomNumber > 50) {
         GameObject spawnedCoin = Instantiate(foodPrefab, transform.position, Quaternion.identity);
         }
    }
}
