using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    [Header("敵のステータス")]
    [SerializeField] int EnemyHealth = 100;
    [SerializeField] [Range(1, 100)] int EnemyAttack = 10;

    // Update is called once per frame
    void Update()
    {
        if (EnemyHealth <= 0)
        {
            Debug.Log("敵を倒しました");
        }
    }

    public void Damage()
    {
        EnemyHealth -= EnemyAttack;
    }
}
