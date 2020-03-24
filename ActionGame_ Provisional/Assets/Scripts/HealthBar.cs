using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    RectTransform rectTransform;

    [SerializeField] Gradient colorGradient;
    [SerializeField] Image fill;

    [SerializeField] Image RightFill;

    [SerializeField] Color endColor;
    [SerializeField] Image animatedBorder;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        fill.color = colorGradient.Evaluate(1f);

        if (RightFill != null)
        {
            RightFill.color = colorGradient.Evaluate(1f);
        }
    }

    public void SetNowHealth(float ratioOfHealth)
    {
        fill.color = colorGradient.Evaluate(ratioOfHealth);
        fill.fillAmount = ratioOfHealth;

        if(RightFill != null)
        {
            RightFill.color = colorGradient.Evaluate(ratioOfHealth);
            RightFill.fillAmount = ratioOfHealth;
        }

        if (animatedBorder != null)
        {
            rectTransform.DOShakePosition(1f, 20f);

            Sequence sequence = DOTween.Sequence();

            sequence.Append(
                animatedBorder.rectTransform.DOPunchScale(new Vector3(1.2f,1.2f),0.2f)
            );

            sequence.Join(
                animatedBorder.DOFade(1f, 0.2f).SetEase(Ease.InFlash).SetLoops(3)
            );

            sequence.Play().OnComplete(() => {
                animatedBorder.color = endColor;
                animatedBorder.rectTransform.localScale = Vector3.one;
            });
        }
    }
}
