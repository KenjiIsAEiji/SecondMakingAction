using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticlChanger : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Image NomalModeImage;
    [SerializeField] Image LRModeImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((int)playerController.NowPlayerState == 2)
        {
            LRModeImage.enabled = true;
            NomalModeImage.enabled = false;
        }
        else
        {
            LRModeImage.enabled = false;
            NomalModeImage.enabled = true;
        }
    }
}
