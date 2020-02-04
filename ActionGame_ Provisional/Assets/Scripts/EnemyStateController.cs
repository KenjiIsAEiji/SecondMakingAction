using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateController : MonoBehaviour
{
    NavMeshAgent EnemyAgent;
    Transform PlayerTransform;

    [Header("敵のHP")]
    [SerializeField] int EnemyHealth = 100;

    enum EnemyState
    {
        Ready = 0,
        Move = 1,
        Attack = 2,
        Dead = 3
    }

    EnemyState NowEnemyState;
    
    // Start is called before the first frame update
    void Start()
    {
        NowEnemyState = EnemyState.Move;
        EnemyAgent = GetComponent<NavMeshAgent>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        switch (NowEnemyState)
        {
            case EnemyState.Ready:
                
                break;

            case EnemyState.Move:
                EnemyAgent.destination = PlayerTransform.position;
                
                //遷移条件
                if (EnemyHealth <= 0) NowEnemyState = EnemyState.Dead;
                break;

            case EnemyState.Attack:

                break;

            case EnemyState.Dead:

                //EnemyAgent.ResetPath(); //Agent停止

                EnemyAgent.enabled = false;

                Debug.Log(gameObject.name + " is Dead");
                DeadAction();
                Destroy(this.gameObject, 10.0f);
                break;
        }
    }
    /// <summary>
    /// 倒れる時のエフェクトなどの呼び出し
    /// </summary>
    void DeadAction()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = true;
        rigidbody.constraints = RigidbodyConstraints.None;
    }

    /// <summary>
    /// 敵のHPをダメージによって減らす
    /// </summary>
    /// <param name="damegeValue">減らすHP</param>
    public void Damage(int damegeValue)
    {
        EnemyHealth -= damegeValue;
    }
}
