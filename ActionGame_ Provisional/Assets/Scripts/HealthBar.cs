using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    RectTransform rectTransform;

    [SerializeField] Gradient colorGradient;
    [SerializeField] Image fill;
    [SerializeField] Color FlshColor;
    [SerializeField] Image animatedBorder;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

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

        if (animatedBorder != null)
        {
            rectTransform.DOShakePosition(1f, 10f);

            Sequence sequence = DOTween.Sequence();

            sequence.Append(
                animatedBorder.rectTransform.DOPunchScale(new Vector3(1.2f,1.2f),0.2f)
            );

            sequence.Join(
                animatedBorder.DOFade(1f, 0.2f).SetEase(Ease.InFlash).SetLoops(3)
            );

            sequence.Play().OnComplete(() => {
                animatedBorder.color = FlshColor;
                animatedBorder.rectTransform.localScale = Vector3.one;
            });
        }
    }
}
