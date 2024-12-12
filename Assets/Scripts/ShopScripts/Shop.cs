using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    GameManager gm;
    Button button;
    public GameObject PriceDisplay;
    public bool canBuy = true;
    public bool bought = false;
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
        if (bought == false){
            PriceDisplay.SetActive(true);
            if (gm.data.money >= Cost && canBuy == true)
            {
                button.interactable = true;
            }
        }
        if (bought == true)
        {
            button.interactable = true;
            PriceDisplay.SetActive(false);
        }
    }

    public void TryToBuy()
    {
        if (bought == false){   
            if (gm.data.money >= Cost)
            {
                gm.Bank(-Cost);
                canBuy = false;
                bought = true;
                SaveData();
            }
        }
    }

    public void SaveData()
{
    PlayerPrefs.SetInt("canBuy", canBuy ? 1 : 0);
    PlayerPrefs.SetInt("bought", bought ? 1 : 0);
    PlayerPrefs.Save();
}

public void LoadData()
{
    canBuy = PlayerPrefs.GetInt("canBuy") == 1 ? true : false;
    bought = PlayerPrefs.GetInt("bought") == 1 ? true : false;
}
}
