using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop3 : MonoBehaviour
{
    GameManager gm;
    Button button;
    public GameObject PriceDisplay;
    public bool canBuy3 = true;
    public bool bought3 = false;
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
        if (bought3 == false){
            PriceDisplay.SetActive(true);
            if (gm.data.money >= Cost && canBuy3 == true)
            {
                button.interactable = true;
            }
        }
        if (bought3 == true)
        {
            button.interactable = true;
            PriceDisplay.SetActive(false);
        }
    }

    public void TryToBuy()
    {
        if (bought3 == false){   
            if (gm.data.money >= Cost)
            {
                gm.Bank(-Cost);
                canBuy3 = false;
                bought3 = true;
                SaveData();
            }
        }
    }

    public void SaveData()
{
    PlayerPrefs.SetInt("canBuy3", canBuy3 ? 1 : 0);
    PlayerPrefs.SetInt("bought3", bought3 ? 1 : 0);
    PlayerPrefs.Save();
}

    public void LoadData()
    {
        canBuy3 = PlayerPrefs.GetInt("canBuy3") == 1 ? true : false;
        bought3 = PlayerPrefs.GetInt("bought3") == 1 ? true : false;
    }
}
