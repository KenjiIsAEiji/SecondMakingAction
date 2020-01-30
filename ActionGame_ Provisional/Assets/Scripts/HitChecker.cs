using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    [Header("ヒットエフェクト")]
    [SerializeField] GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit!!");
            Quaternion effectSponeAngle = Quaternion.FromToRotation(Vector3.forward, collision.contacts[0].normal);
            Instantiate(effect, collision.contacts[0].point, effectSponeAngle);
        }
    }
}
