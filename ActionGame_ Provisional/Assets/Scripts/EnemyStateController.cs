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

    [Header("- 敵のHP -")]
    public float EnemyMaxHealth = 100;
    public float EnemyCurrentHealth = 100;

    [Header("- 敵のヒットストップ設定 -")]
    [SerializeField] float HSScale = 0.1f;
    //[SerializeField] float HS_BaceTime = 1f;

    [Header("- HPバー -")]
    [SerializeField] HealthBar healthBar;

    [Header("- 近接攻撃する距離 -")]
    [SerializeField] float attackDistance = 1.2f;

    [Header("- 敵からの攻撃用 -")]
    [SerializeField] float EnemyAttackPower = 10;
    [SerializeField] GameObject hand;

    [Header("- ノックバックの設定 -")]
    [SerializeField] float KickBackForce = 10.0f;
    [SerializeField] float KickBackTime = 1f;

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

        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        EnemyCurrentHealth = EnemyMaxHealth;

        EnemyAgent = GetComponent<NavMeshAgent>();
        EnemyAnimator = GetComponent<Animator>();
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
                //StopCoroutine("HitStopEffect");
                StopCoroutine("NoDamageTimer");

                float diff = (transform.position - PlayerTransform.position).magnitude;
                
                //遷移条件
                if (EnemyCurrentHealth <= 0)
                {
                    NowEnemyState = EnemyState.Dead;
                }
                else if (diff < attackDistance)
                {
                    NowEnemyState = EnemyState.Attack;
                }

                EnemyAgent.updateRotation = true;

                EnemyAgent.SetDestination(PlayerTransform.position);

                break;

            case EnemyState.Attack:
                EnemyAgent.ResetPath();
                EnemyAgent.velocity = Vector3.zero;
                EnemyAgent.updateRotation = false;
                PlayerLookOn();

                if (EnemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("MoveTree"))
                {
                    NowEnemyState = EnemyState.Move;
                }

                break;

            case EnemyState.KickBack:
                EnemyAgent.ResetPath();

                EnemyAgent.updateRotation = false;
                PlayerLookOn();

                break;

            case EnemyState.Dead:

                //EnemyAgent.ResetPath(); //Agent停止

                EnemyAgent.enabled = false;
                GetComponent<Collider>().enabled = false;

                Debug.Log(gameObject.name + " is Dead");
                Destroy(this.gameObject, 10.0f);
                break;
        }
        EnemyAnimator.SetFloat("MoveSpeed", EnemyAgent.velocity.magnitude);
        EnemyAnimator.SetInteger("EnemyState", (int)NowEnemyState);
    }

    private void PlayerLookOn()
    {
        Quaternion rot = Quaternion.LookRotation(-(transform.position - PlayerTransform.position), transform.up);
        transform.rotation = Quaternion.AngleAxis(rot.eulerAngles.y, Vector3.up);
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="damegeValue">ダメージ量</param>
    /// <param name="damegeScale">モーションごとのダメージ倍率</param>
    public void Damage(float damegeValue,float damegeScale)
    {
        if(NowEnemyState != EnemyState.Dead)
        {
            EnemyCurrentHealth -= damegeValue * damegeScale;

            if (EnemyCurrentHealth <= 0)
            {
                EnemyCurrentHealth = 0;
                StartCoroutine(DeadAction(1f));

                SpawnManager.Instance.DestroyEnemy(this.gameObject);
                NowEnemyState = EnemyState.Dead;
            }
            else
            {
                //StartCoroutine(HitStopEffect(HS_BaceTime * Mathf.Pow(damegeScale, 2)));
                StartCoroutine(NoDamageTimer(damegeScale));
                NowEnemyState = EnemyState.KickBack;
            }
            healthBar.SetNowHealth(EnemyCurrentHealth / EnemyMaxHealth,true);
        }
    }

    void KickBackMove()
    {
        EnemyAnimator.SetTrigger("KickBack");
        Vector3 dir = (transform.position - PlayerTransform.position).normalized;
        EnemyAgent.velocity = dir * KickBackForce;
    }

    IEnumerator NoDamageTimer(float Scale)
    {
        KickBackMove();

        yield return new WaitForSecondsRealtime(KickBackTime * Scale);

        NowEnemyState = EnemyState.Move;
    }

    IEnumerator DeadAction(float _time)
    {
        Time.timeScale = HSScale;

        yield return new WaitForSecondsRealtime(_time);

        Time.timeScale = 1f;
    }

    //IEnumerator HitStopEffect(float stopTime)
    //{
    //    Time.timeScale = HSScale;

    //    yield return new WaitForSecondsRealtime(stopTime);

    //    Time.timeScale = 1f;
    //}

    void AttackEnter()
    {
        //Debug.Log("Enemy Attack Start");
        hand.GetComponent<Collider>().enabled = true;
    }

    public void AttackExit()
    {
        //Debug.Log("Enemy Attack End");
        hand.GetComponent<Collider>().enabled = false;
    }
}
