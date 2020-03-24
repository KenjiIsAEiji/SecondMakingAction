using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("プレイヤーのステータス")]
    [SerializeField] float MaxPlayerLifePoint = 200;
    [SerializeField] float CurrentPlayerLifePoint;
    [SerializeField] float PlayerMeleeAttack = 20;
    [SerializeField] float PlayerRangedAttack = 50;
    [SerializeField] float MaxShieldHealth = 100;
    [SerializeField] float CurrentShieldHealth;
    [SerializeField] float ShieldCreate = 50;
    // Start is called before the first frame update
    void Start()
    {
        CurrentPlayerLifePoint = MaxPlayerLifePoint;
        CurrentShieldHealth = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentPlayerLifePoint <= 0)
        {
            Debug.Log("GAMEOVER");
        }

        MeleeAttack();
        RangedAttack();
        Shield();
    }

    public void MeleeAttack()
    {
        if (CurrentPlayerLifePoint > 1)//LPが２以上の時近接攻撃が可能
        {

            if (Input.GetKeyDown(KeyCode.E))//攻撃があたった判定
            {
                CurrentPlayerLifePoint -= 1;//近接攻撃した時にLP-1
                Debug.Log("-1");
            }
        }
    }
    public void RangedAttack()
    {
        if (Input.GetKey(KeyCode.LeftShift))//LShiftを押してるか判定
            if (CurrentPlayerLifePoint > 10)//LPが11以上のとき遠距離攻撃可能
            {
                if (Input.GetKeyDown(KeyCode.R))//遠距離攻撃をしたか判定
                {
                    CurrentPlayerLifePoint -= 10;
                    Debug.Log("-10");
                }
            }
    }

    public void Shield()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (CurrentShieldHealth > 0)
            {
                Debug.Log("シールド展開");
            }

            if (CurrentShieldHealth <= 0)
            {
                if (CurrentPlayerLifePoint >= 51)
                {
                    CurrentPlayerLifePoint -= ShieldCreate;
                    CurrentShieldHealth = MaxShieldHealth;
                    Debug.Log("シールド生成");
                    Debug.Log("シールド展開");
                }
            }
        }
        if (Input.GetKey(KeyCode.Q))//攻撃されたことを判定
        {
            CurrentShieldHealth -= 10;//シールドの耐久値減らす
        }
    }
}