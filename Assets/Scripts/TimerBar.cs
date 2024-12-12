using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    // Start is called before the first frame update
    public void SetMaxTime (float time) {
        slider.maxValue = time;
        slider.value = time;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetTime(float time) {
        slider.value = time;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
