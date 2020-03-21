using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollider : MonoBehaviour
{
    public float AttackPower = 10;
    [SerializeField] Transform EnemyTransform;
    [SerializeField] GameObject PlayerHitEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Damage(AttackPower,EnemyTransform.forward);
            Instantiate(PlayerHitEffect, collision.contacts[0].point, Quaternion.identity);
        }
    }
}
