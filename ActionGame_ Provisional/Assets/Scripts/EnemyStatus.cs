using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    public GameObject EnemyDelete = null;

    [Header("敵のステータス")]
    [SerializeField]
    [Range(10, 1000)]
    int EnemyHealth = 100;
    [SerializeField]
    [Range(1, 100)]
    int EnemyAttack = 10;


   
    // Update is called once per frame
    void Update()
    {
        Text EnemyText = EnemyDelete.GetComponent<Text>();

        if (EnemyHealth <= 0)
        {
            Debug.Log("敵を倒しました");
            EnemyText.text = ("敵を倒しました。");
        }
    }
}
