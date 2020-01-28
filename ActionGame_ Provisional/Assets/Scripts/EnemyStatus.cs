using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    public GameObject Enemy;
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
        if (Input.GetKey(KeyCode.A))
        {
            EnemyHealth = EnemyHealth-5;
            if (EnemyHealth <= 0)
            {
                Debug.Log("敵を倒しました");
                Destroy(Enemy);
            }
        }


    }
}
