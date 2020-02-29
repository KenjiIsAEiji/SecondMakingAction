using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyStateController : MonoBehaviour
{
    NavMeshAgent EnemyAgent;
    Transform PlayerTransform;

    [Header("敵のHP")]
    public float EnemyMaxHealth = 100;
    public float EnemyCurrentHealth = 100;

    [Header("敵のヒットストップ設定")]
    [SerializeField] float HSScale = 0.1f;
    [SerializeField] float HS_BaceTime = 1f;

    [Header("HPバー")]
    [SerializeField] HealthBar healthBar;


    Animator EnemyAnimator;

    enum EnemyState
    {
        Ready = 0,
        Move = 1,
        Attack = 2,
        KickBack = 3,
        Dead = 4
    }

    [SerializeField] EnemyState NowEnemyState;
    
    // Start is called before the first frame update
    void Start()
    {
        NowEnemyState = EnemyState.Move;

        EnemyCurrentHealth = EnemyMaxHealth;
        healthBar.SetMaxHealth(EnemyMaxHealth);

        EnemyAgent = GetComponent<NavMeshAgent>();
        EnemyAnimator = GetComponent<Animator>();
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
                //ノックバックコルーチンを停止
                StopCoroutine("HitStopEffect");

                EnemyAgent.destination = PlayerTransform.position;

                EnemyAnimator.SetFloat("MoveSpeed", EnemyAgent.velocity.magnitude);

                //遷移条件
                if (EnemyCurrentHealth <= 0) NowEnemyState = EnemyState.Dead;

                break;

            case EnemyState.Attack:

                break;

            case EnemyState.KickBack:
                EnemyAgent.ResetPath();
                EnemyAgent.velocity = Vector3.zero;


                break;

            case EnemyState.Dead:

                //EnemyAgent.ResetPath(); //Agent停止

                EnemyAgent.enabled = false;

                Debug.Log(gameObject.name + " is Dead");
                //DeadAction();
                Destroy(this.gameObject, 10.0f);
                break;
        }
        EnemyAnimator.SetInteger("EnemyState", (int)NowEnemyState);
    }

    /// <summary>
    /// 倒れる時のエフェクトなどの呼び出し
    /// </summary>
    //void DeadAction()
    //{
    //    Rigidbody rigidbody = GetComponent<Rigidbody>();
    //    rigidbody.useGravity = true;
    //    rigidbody.constraints = RigidbodyConstraints.None;
    //    rigidbody.AddForce(-transform.forward * 10.0f * HSScale);
    //}

    /// <summary>
    /// 敵のHPをダメージによって減らす
    /// </summary>
    /// <param name="damegeValue">減らすHP</param>
    public void Damage(float damegeValue,float damegeScale)
    {
        if(NowEnemyState != EnemyState.KickBack && NowEnemyState != EnemyState.Dead)
        {
            EnemyCurrentHealth -= damegeValue * damegeScale;

            if (EnemyCurrentHealth <= 0) EnemyCurrentHealth = 0;
            healthBar.SetNowHealth(EnemyCurrentHealth);

            StartCoroutine(HitStopEffect(HS_BaceTime * damegeScale * damegeScale));
        }
    }

    IEnumerator HitStopEffect(float stopTime)
    {
        Time.timeScale = HSScale;
        NowEnemyState = EnemyState.KickBack;
        
        Debug.Log("now enemy hit stop");

        yield return new WaitForSecondsRealtime(stopTime);

        Time.timeScale = 1f;
        NowEnemyState = EnemyState.Move;
    }
}
