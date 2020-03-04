using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;

public class CamEffect : MonoBehaviour
{
    Volume volume;
    CinemachineImpulseSource impulseSource;
    

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageEffect(float weight)
    {
        volume.weight = weight;
        impulseSource.GenerateImpulse();
    }
}
