using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;
    //public float maxStamina;
    float currentStamina;
    public Image fill;

    // Start is called before the first frame update
    public void SetMaxStamina (float stamina) {
        slider.maxValue = stamina;
        slider.value = stamina;
    }

    public void SetStamina(float stamina) {
        currentStamina = stamina;
        slider.value = stamina;
    }

    // Update is called once per frame
    
}
