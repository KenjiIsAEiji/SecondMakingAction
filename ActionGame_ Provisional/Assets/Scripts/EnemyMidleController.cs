using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMidleController : MonoBehaviour
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

    [Header("- 敵からの遠距攻撃用 -")]
    [SerializeField] EnemyShotShell enemyShotShell;
    //[SerializeField] float EnemyAttackPower = 10;

    [Header("- ノックバックの設定 -")]
    [SerializeField] float KickBackForce = 10.0f;
    [SerializeField] float KickBackTime = 1f;

    //Animator EnemyAnimator;

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
        healthBar.SetMaxHealth(EnemyCurrentHealth);

        EnemyAgent = GetComponent<NavMeshAgent>();
        //EnemyAnimator = GetComponent<Animator>();
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

                //遷移条件
                if (EnemyCurrentHealth <= 0)
                {
                    NowEnemyState = EnemyState.Dead;
                }
                else if (!EnemyAgent.Raycast(PlayerTransform.position, out NavMeshHit navMeshHit))
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

                if (EnemyAgent.Raycast(PlayerTransform.position, out NavMeshHit Hit))
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

        enemyShotShell.enabled = (NowEnemyState == EnemyState.Attack);

        //EnemyAnimator.SetFloat("MoveSpeed", EnemyAgent.velocity.magnitude);
        //EnemyAnimator.SetInteger("EnemyState", (int)NowEnemyState);
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
    public void Damage(float damegeValue, float damegeScale)
    {
        if (NowEnemyState != EnemyState.Dead)
        {
            EnemyCurrentHealth -= damegeValue * damegeScale;

            if (EnemyCurrentHealth <= 0)
            {
                EnemyCurrentHealth = 0;
                StartCoroutine(DeadAction(1f));
                NowEnemyState = EnemyState.Dead;
            }
            else
            {
                //StartCoroutine(HitStopEffect(HS_BaceTime * Mathf.Pow(damegeScale, 2)));
                StartCoroutine(NoDamageTimer());
                NowEnemyState = EnemyState.KickBack;
            }
            healthBar.SetNowHealth(EnemyCurrentHealth);
        }
    }
    void KickBackMove()
    {
        //EnemyAnimator.SetTrigger("KickBack");
        EnemyAgent.velocity = PlayerTransform.forward * KickBackForce;
    }

    IEnumerator NoDamageTimer()
    {
        KickBackMove();

        yield return new WaitForSecondsRealtime(KickBackTime);

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

}
