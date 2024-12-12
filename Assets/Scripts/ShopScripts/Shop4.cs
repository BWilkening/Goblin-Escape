using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop4 : MonoBehaviour
{
    GameManager gm;
    Button button;
    public GameObject PriceDisplay;
    public bool canBuy4 = true;
    public bool bought4 = false;
    public int Cost;


    private void Awake() {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        button = GetComponent<Button>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        button.interactable = false;
        LoadData();
    }

    public void Update()
    {
        if (bought4 == false){
            PriceDisplay.SetActive(true);
            if (gm.data.money >= Cost && canBuy4 == true)
            {
                button.interactable = true;
            }
        }
        if (bought4 == true)
        {
            button.interactable = true;
            PriceDisplay.SetActive(false);
        }
    }

    public void TryToBuy()
    {
        if (bought4 == false){   
            if (gm.data.money >= Cost)
            {
                gm.Bank(-Cost);
                canBuy4 = false;
                bought4 = true;
                SaveData();
            }
        }
    }

    public void SaveData()
{
    PlayerPrefs.SetInt("canBuy4", canBuy4 ? 1 : 0);
    PlayerPrefs.SetInt("bought4", bought4 ? 1 : 0);
    PlayerPrefs.Save();
}

    public void LoadData()
    {
        canBuy4 = PlayerPrefs.GetInt("canBuy4") == 1 ? true : false;
        bought4 = PlayerPrefs.GetInt("bought4") == 1 ? true : false;
    }

}
