using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop8 : MonoBehaviour
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
        loadData();
    }

    public void Update()
    {
        if (bought == false){
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
                saveData();
            }
        }
    }

    void saveData()
{
    PlayerPrefs.SetInt("canBuy", boolToInt(canBuy));
    PlayerPrefs.SetInt("bought", boolToInt(bought));
}

void loadData()
{
    canBuy = intToBool(PlayerPrefs.GetInt("canBuy", 1));
    bought = intToBool(PlayerPrefs.GetInt("bought", 0));
}

    int boolToInt(bool val)
    {
    if (val)
        return 1;
    else
        return 0;
    }

    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }
}
