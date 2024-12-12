using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop6 : MonoBehaviour
{
    GameManager gm;
    Button button;
    public GameObject PriceDisplay;
    public bool canBuy6 = true;
    public bool bought6 = false;
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
        if (bought6 == false){
            PriceDisplay.SetActive(true);
            if (gm.data.money >= Cost && canBuy6 == true)
            {
                button.interactable = true;
            }
        }
        if (bought6 == true)
        {
            button.interactable = true;
            PriceDisplay.SetActive(false);
        }
    }

    public void TryToBuy()
    {
        if (bought6 == false){   
            if (gm.data.money >= Cost)
            {
                gm.Bank(-Cost);
                canBuy6 = false;
                bought6 = true;
                SaveData();
            }
        }
    }

    public void SaveData()
{
    PlayerPrefs.SetInt("canBuy6", canBuy6 ? 1 : 0);
    PlayerPrefs.SetInt("bought6", bought6 ? 1 : 0);
    PlayerPrefs.Save();
}

    public void LoadData()
    {
        canBuy6 = PlayerPrefs.GetInt("canBuy6") == 1 ? true : false;
        bought6 = PlayerPrefs.GetInt("bought6") == 1 ? true : false;
    }
}
