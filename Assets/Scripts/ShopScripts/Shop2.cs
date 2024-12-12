using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop2 : MonoBehaviour
{
    GameManager gm;
    Button button;
    public GameObject PriceDisplay;
    public bool canBuy2 = true;
    public bool bought2 = false;
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
        if (bought2 == false){
            PriceDisplay.SetActive(true);
            if (gm.data.money >= Cost && canBuy2 == true)
            {
                button.interactable = true;
            }
        }
        if (bought2 == true)
        {
            button.interactable = true;
            PriceDisplay.SetActive(false);
        }
    }

    public void TryToBuy()
    {
        if (bought2 == false){   
            if (gm.data.money >= Cost)
            {
                gm.Bank(-Cost);
                canBuy2 = false;
                bought2 = true;
                SaveData();
            }
        }
    }

    public void SaveData()
{
    PlayerPrefs.SetInt("canBuy2", canBuy2 ? 1 : 0);
    PlayerPrefs.SetInt("bought2", bought2 ? 1 : 0);
    PlayerPrefs.Save();
}

public void LoadData()
{
    canBuy2 = PlayerPrefs.GetInt("canBuy2") == 1 ? true : false;
    bought2 = PlayerPrefs.GetInt("bought2") == 1 ? true : false;
}
}

