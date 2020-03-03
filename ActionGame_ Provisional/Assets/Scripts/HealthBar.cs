using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    [SerializeField] Gradient colorGradient;
    [SerializeField] Image fill;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = colorGradient.Evaluate(1f);
    }

    public void SetNowHealth(float health)
    {
        slider.value = health;
        fill.color = colorGradient.Evaluate(slider.normalizedValue);
    }
}
