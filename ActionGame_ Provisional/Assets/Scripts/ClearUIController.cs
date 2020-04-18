using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ClearUIController : MonoBehaviour
{
    CanvasGroup canvasGroup;

    [SerializeField] Text MainText;
    [SerializeField] Text score1;
    [SerializeField] Text score2;
    [SerializeField] Text score3;

    [SerializeField] float UIShadowDistance = 8.0f;
    [SerializeField] float sendTextBaseTime = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        Shadow textShadow = MainText.GetComponent<Shadow>();
        textShadow.effectDistance = Vector2.zero;

        MainText.text = "";
        score1.text = score2.text = score3.text = "";

        Sequence sequence = DOTween.Sequence();

        sequence.Append(canvasGroup.DOFade(1f, 1f).SetEase(Ease.Linear));
        sequence.Append(MainText.DOText("MISSION CLEAR", "MISSION CLEAR".Length * sendTextBaseTime).SetEase(Ease.Linear));
        

        Tween UIShadowTween = DOTween.To(
                () => textShadow.effectDistance,
                vec2 => textShadow.effectDistance = vec2,
                new Vector2(1,-1) * UIShadowDistance,
                0.1f
            );

        sequence.Append(UIShadowTween.SetEase(Ease.InFlash));

        sequence.AppendInterval(0.5f);

        string score1txt = "<b>撃破数</b>\n"
            + "\t通常型　\t\t" + GameManager.Instance.nomalEnemyDestroys + "体\t\t" + GameManager.Instance.NomalDestroyPoints + "p\n"
            + "\t遠距離型\t\t" + GameManager.Instance.longRangeEnemyDestroys + "体\t\t" + GameManager.Instance.LongRangeDestroyPoints + "p";

        sequence.Append(score1.DOText(score1txt, score1txt.Length * sendTextBaseTime).SetEase(Ease.Linear));

        sequence.AppendInterval(0.3f);

        string score2txt = "<b>残存LPボーナス</b>\t\t" + GameManager.Instance.LPRemaingPoints + "p";
        sequence.Append(score2.DOText(score2txt, score2txt.Length * sendTextBaseTime).SetEase(Ease.Linear));

        sequence.AppendInterval(0.3f);

        int min = (int)(GameManager.Instance.fightTime / 60);
        float sec = (GameManager.Instance.fightTime % 60);

        string score3txt = "<b>クリアタイムボーナス</b>\n"
            + "クリアタイム\t\t" + min + ":" + sec.ToString("00.00") + "\t\t" + GameManager.Instance.PassingTimePoints + "p";

        sequence.Append(score3.DOText(score3txt, score3txt.Length * sendTextBaseTime).SetEase(Ease.Linear));

        int tortalScore =
            GameManager.Instance.NomalDestroyPoints +
            GameManager.Instance.LongRangeDestroyPoints +
            GameManager.Instance.LPRemaingPoints +
            GameManager.Instance.PassingTimePoints;

        Debug.Log("<color=#0000ffff>TortalScore " + tortalScore + "</color>");

        sequence.Play();
    }
}
