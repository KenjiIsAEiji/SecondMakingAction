using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GOAnimation : MonoBehaviour
{
    Image MainPanel;
    [SerializeField] Text GOText;

    // Start is called before the first frame update
    void Start()
    {
        MainPanel = GetComponent<Image>();
        Sequence sequence = DOTween.Sequence();

        sequence.Append(MainPanel.DOFade(.5f, 1f));

        sequence.Append(GOText.DOText("Game Over", 2f));

        sequence.Play();
    }
}
