using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;
    [SerializeField] Gradient colorGradient;
    [SerializeField] Image fill;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        fill.color = colorGradient.Evaluate(1f);
    }

    public void SetNowHealth(float health)
    {
        slider.value = health;
        fill.color = colorGradient.Evaluate(slider.normalizedValue);
    }
}
