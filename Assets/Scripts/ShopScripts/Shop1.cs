using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop1 : MonoBehaviour
{
    GameManager gm;
    Button button;
    public GameObject PriceDisplay;
    public bool canBuy1 = true;
    public bool bought1 = false;
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
        if (bought1 == false){
            PriceDisplay.SetActive(true);
            if (gm.data.money >= Cost && canBuy1 == true)
            {
                button.interactable = true;
            }
        }
        if (bought1 == true)
        {
            button.interactable = true;
            PriceDisplay.SetActive(false);
        }
    }

    public void TryToBuy()
    {
        if (bought1 == false){   
            if (gm.data.money >= Cost)
            {
                gm.Bank(-Cost);
                canBuy1 = false;
                bought1 = true;
                SaveData();
            }
        }
    }

    public void SaveData()
{
    PlayerPrefs.SetInt("canBuy1", canBuy1 ? 1 : 0);
    PlayerPrefs.SetInt("bought1", bought1 ? 1 : 0);
    PlayerPrefs.Save();
}

    public void LoadData()
    {
        canBuy1 = PlayerPrefs.GetInt("canBuy1") == 1 ? true : false;
        bought1 = PlayerPrefs.GetInt("bought1") == 1 ? true : false;
    }
}

