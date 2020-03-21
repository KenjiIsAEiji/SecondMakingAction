using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSlash : MonoBehaviour
{
    [SerializeField] float AttackPower = 5f;
    [SerializeField] GameObject HitEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyStateController>().Damage(AttackPower, 5f);
            Instantiate(HitEffect, other.ClosestPointOnBounds(this.transform.position), Quaternion.identity);
        }

        if (other.gameObject.CompareTag("MidleEnemy"))
        {
            other.gameObject.GetComponent<EnemyMidleController>().Damage(AttackPower, 5f);
            Instantiate(HitEffect, other.ClosestPointOnBounds(this.transform.position), Quaternion.identity);
        }
    }
}
