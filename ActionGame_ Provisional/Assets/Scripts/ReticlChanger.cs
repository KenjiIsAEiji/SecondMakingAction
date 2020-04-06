using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ReticlChanger : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Image NomalModeImage;
    [SerializeField] Image LRModeImage;

    [SerializeField] GameObject GameOverUI;

    [SerializeField] Image CanShieldImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.ShieldCurrentHealth > 0)
        {
            CanShieldImage.enabled = true;
        }
        else
        {
            CanShieldImage.enabled = false;
        }
        
        if((int)playerController.NowPlayerState == 2)
        {
            LRModeImage.enabled = true;
            NomalModeImage.enabled = false;
        }
        else if ((int)playerController.NowPlayerState == 5)
        {
            GameOverUI.SetActive(true);
            LRModeImage.enabled = false;
            NomalModeImage.enabled = false;
        }
        else
        {
            LRModeImage.enabled = false;
            NomalModeImage.enabled = true;
        }
    }
}
