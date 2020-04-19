using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] int LoadSceneIndex = 1;
    [SerializeField] GameObject Door;
    [SerializeField] Image InteractProgressImg;
    [SerializeField] Image KeyImage;
    [SerializeField] float interactTime;
    private float t = 0.0f;

    private void Awake()
    {
        InteractProgressImg.rectTransform.localScale = Vector3.zero;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F)) 
            {
                KeyImage.rectTransform.DOPunchScale(Vector3.one * 0.5f, 0.1f).OnComplete(() =>
                {
                    KeyImage.rectTransform.localScale = Vector3.one;
                });
            }

            if (Input.GetKey(KeyCode.F))
            {
                t += Time.deltaTime;

                InteractProgressImg.fillAmount = t / interactTime;

                if (t >= interactTime)
                {
                    UIAnimationAndLoad();
                    this.GetComponent<Collider>().enabled = false;
                }
            }
            else
            {
                t = 0;
                InteractProgressImg.DOFillAmount(0, 0.1f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) InteractProgressImg.rectTransform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InBack);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) InteractProgressImg.rectTransform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
    }

    void UIAnimationAndLoad()
    {
        InteractProgressImg.rectTransform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InBack).OnComplete(() =>
        {
            InteractProgressImg.gameObject.SetActive(false);
            StartCoroutine(LoadData());
        });
    }

    IEnumerator LoadData()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1,LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log("Load progress" + progress);

            yield return null;
        }
        DoorAnimation();
    }

    void DoorAnimation()
    {
        Door.transform.DOLocalMove(Door.transform.localPosition + Vector3.left, 0.5f).SetEase(Ease.OutBack);
    }
}
