using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;

public class CamEffect : MonoBehaviour
{
    CinemachineImpulseSource impulseSource;

    [SerializeField] Volume DamageEffectVol;
    [SerializeField] Volume KickBackEffectVol;
    
    // Start is called before the first frame update
    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void DamageEffect(float weight)
    {
        DamageEffectVol.weight = weight;
    }

    public void KickBackEffect(float weight)
    {
        impulseSource.GenerateImpulse();
        DamageEffectVol.weight = weight;
        KickBackEffectVol.weight = weight;
    }
}
