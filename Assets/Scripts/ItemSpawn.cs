using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] itemPrefab;

    ItemDrop itemDrop;
    
    private void Awake() {
        itemDrop = gameObject.GetComponent<ItemDrop>();
    }

    public void common() {
        GameObject spawnedCoin = Instantiate(itemPrefab[0], transform.position, Quaternion.identity);
    }

    public void rare() {
        GameObject spawnedCoin = Instantiate(itemPrefab[1], transform.position, Quaternion.identity);
    }

    public void SpawnCoin() {
        float randomNumber = Random.Range(0, 100);
        Debug.Log(randomNumber);

        if (randomNumber > 50 && randomNumber <= 90) {
        common();
        }

         if (randomNumber > 90 && randomNumber <= 99) {
        rare();
        }
    }
}
