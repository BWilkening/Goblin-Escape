using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop7 : MonoBehaviour
{
    GameManager gm;
    Button button;
    public GameObject PriceDisplay;
    public bool canBuy7 = true;
    public bool bought7 = false;
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
        if (bought7 == false){
            PriceDisplay.SetActive(true);
            if (gm.data.money >= Cost && canBuy7 == true)
            {
                button.interactable = true;
            }
        }
        if (bought7 == true)
        {
            button.interactable = true;
            PriceDisplay.SetActive(false);
        }
    }

    public void TryToBuy()
    {
        if (bought7 == false){   
            if (gm.data.money >= Cost)
            {
                gm.Bank(-Cost);
                canBuy7 = false;
                bought7 = true;
                SaveData();
            }
        }
    }

    public void SaveData()
{
    PlayerPrefs.SetInt("canBuy7", canBuy7 ? 1 : 0);
    PlayerPrefs.SetInt("bought7", bought7 ? 1 : 0);
    PlayerPrefs.Save();
}

    public void LoadData()
    {
        canBuy7 = PlayerPrefs.GetInt("canBuy7") == 1 ? true : false;
        bought7 = PlayerPrefs.GetInt("bought7") == 1 ? true : false;
    }
}
