using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    RectTransform rectTransform;

    [Header("メインバー(必須)")]
    [SerializeField] Gradient colorGradient;
    [SerializeField] Image fill;

    [SerializeField] Image BackGroundBar;
    [SerializeField] Color DamageColor;

    [Header("右側バー")]
    [SerializeField] Image RightFill;
    [SerializeField] Image BackGroundBarRight;

    [Header("アニメーションするボーダーライン")]
    [SerializeField] Color ABEndColor;
    [SerializeField] Image animatedBorder;

    [Header("パーセンテージ表示")]
    [SerializeField] Text percentage;

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

        BackGroundBar.color = DamageColor;
        BackGroundBar.DOFillAmount(ratioOfHealth, 1f).SetEase(Ease.InQuart);

        if(RightFill != null)
        {
            RightFill.color = colorGradient.Evaluate(ratioOfHealth);
            RightFill.fillAmount = ratioOfHealth;

            BackGroundBarRight.color = DamageColor;
            BackGroundBarRight.DOFillAmount(ratioOfHealth, 1f).SetEase(Ease.InQuart);
        }

        if (animatedBorder != null)
        {
            RectTransform rectBuff = this.rectTransform;
            Sequence sequence = DOTween.Sequence();

            sequence.Append(
                animatedBorder.rectTransform.DOPunchScale(new Vector3(1.2f,1.2f),0.2f)
            );

            sequence.Join(
                animatedBorder.DOFade(1f, 0.2f).SetEase(Ease.InFlash).SetLoops(3)
            );

            sequence.Join(
                rectTransform.DOShakePosition(1f, 20f)
            );

            sequence.Play().OnComplete(() => {
                animatedBorder.color = ABEndColor;
                animatedBorder.rectTransform.localScale = Vector3.one;
                this.rectTransform = rectBuff;
            });
        }

        string per = (ratioOfHealth * 100).ToString("F2") + "%";

        if (percentage != null) percentage.DOText(per, 0.7f);

    }
}
