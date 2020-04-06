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

    [Header("モーションスケール参照用")]
    [SerializeField] CharacterAnimation characterAnimation;

    [Header("LP参照用")]
    [SerializeField] PlayerController playerController;
    [SerializeField] float usePlayerPL = 1;
    float LowLPRaito = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Enemy"))
    //    {
    //        Debug.Log("Hit!!");
    //        Quaternion effectSponeAngle = Quaternion.FromToRotation(Vector3.forward, Vector3.up);
    //        Instantiate(effect, other.ClosestPoint(transform.position), effectSponeAngle);

    //        // Hit時にダメージ処理
    //        other.gameObject.GetComponent<EnemyStateController>().Damage(AttackValue,transform.forward);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("MidleEnemy"))
        {
            float LPRaito;

            if(playerController.PlayerCurrentLP < playerController.PlayerMaxLP / 4)
            {
                LPRaito = LowLPRaito;
            }
            else
            {
                HitLPUse(usePlayerPL);
                LPRaito = 1f;
            }

            if(collision.gameObject.CompareTag("Enemy")) collision.gameObject.GetComponent<EnemyStateController>().Damage(AttackValue, characterAnimation.NowMotionScale * LPRaito);
            else collision.gameObject.GetComponent<EnemyMidleController>().Damage(AttackValue, characterAnimation.NowMotionScale * LPRaito);

            Quaternion effectSponeAngle = Quaternion.FromToRotation(Vector3.forward, collision.contacts[0].normal);
            Instantiate(effect, collision.contacts[0].point, effectSponeAngle);
        }
    }

    void HitLPUse(float useLP)
    {
        playerController.UsingLP(useLP);
    }
}
