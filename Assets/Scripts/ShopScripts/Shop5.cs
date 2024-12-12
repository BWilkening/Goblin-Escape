using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop5 : MonoBehaviour
{
    GameManager gm;
    Button button;
    public GameObject PriceDisplay;
    public bool canBuy5 = true;
    public bool bought5 = false;
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
        if (bought5 == false){
            PriceDisplay.SetActive(true);
            if (gm.data.money >= Cost && canBuy5 == true)
            {
                button.interactable = true;
            }
        }
        if (bought5 == true)
        {
            button.interactable = true;
            PriceDisplay.SetActive(false);
        }
    }

    public void TryToBuy()
    {
        if (bought5 == false){   
            if (gm.data.money >= Cost)
            {
                gm.Bank(-Cost);
                canBuy5 = false;
                bought5 = true;
                SaveData();
            }
        }
    }

    public void SaveData()
{
    PlayerPrefs.SetInt("canBuy5", canBuy5 ? 1 : 0);
    PlayerPrefs.SetInt("bought5", bought5 ? 1 : 0);
    PlayerPrefs.Save();
}

    public void LoadData()
    {
        canBuy5 = PlayerPrefs.GetInt("canBuy5") == 1 ? true : false;
        bought5 = PlayerPrefs.GetInt("bought5") == 1 ? true : false;
    }
}
