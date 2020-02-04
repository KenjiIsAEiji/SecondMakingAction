using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    [Header("ヒットエフェクト")]
    [SerializeField] GameObject effect;

    [Header("攻撃力")]
    [Range(1, 100)]
    [SerializeField] int AttackValue = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit!!");
            Quaternion effectSponeAngle = Quaternion.FromToRotation(Vector3.forward, Vector3.up);
            Instantiate(effect, other.ClosestPoint(transform.position), effectSponeAngle);

            // Hit時にダメージ処理
            other.gameObject.GetComponent<EnemyStateController>().Damage(AttackValue);
        }
    }
    
}
