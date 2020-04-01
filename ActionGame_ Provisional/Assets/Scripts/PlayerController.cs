using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    Rigidbody playerRigidbody;
    public Vector3 MoveVecter = Vector3.zero;

    [Header("- 移動速度・キャラクター回転速度 -")]
    [SerializeField] float Speed = 10.0f;
    [SerializeField] float TurnSpeed = 10.0f;
    [SerializeField] float moveMultiply = 10.0f;

    [Header("- カメラの向き取得用 -")]
    [SerializeField] Transform CameraTransform;

    [Header("- 接地判定用のRay発生位置 -")]
    [SerializeField] Transform RayOrigin;
    [SerializeField] float RayRange = 10;

    [Header("- Playerの状態 -")]
    public PlayerState NowPlayerState;

    [Header("- PlayerのLP -")]
    public float PlayerMaxLP = 100f;
    public float PlayerCurrentLP;
    [SerializeField] HealthBar healthBar;

    [Header("- ノックバック -")]
    [SerializeField] float kickBackStrength = 2f;
    private Vector3 kickBackDrection;
    [SerializeField] CamEffect effect;

    [Header("- 攻撃時の移動 -")]
    [SerializeField] float AttackJump = 20f;

    [Header("シールド展開時")]
    [SerializeField] GameObject ShieldModel;
    [SerializeField] float MaxShieldHealth = 100;
    [SerializeField] float ShieldCurrentHealth;
    [SerializeField] float ShieldCreate = 50;
    [SerializeField] bool ShieldBreak = false;

    public bool Attacking = false;
    public bool IsGrounded;

    Animator playerAnimator;

    public enum PlayerState
    {
        Ready = 0,
        NomalFight = 1,
        LongRange = 2,
        KickBack = 3,
        Shield = 4,
        Dead = 5
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        IsGrounded = false;

        NowPlayerState = PlayerState.NomalFight;
        PlayerCurrentLP = PlayerMaxLP;
        ShieldCurrentHealth = 0;

        effect.DeadEffect(0);
    }

    // Update is called once per frame
    void Update()
    {
        float InputX = Input.GetAxis("Horizontal");
        float InputZ = Input.GetAxis("Vertical");

        MoveVecter = new Vector3(InputX * Speed, 0, InputZ * Speed);

        switch (NowPlayerState)
        {
            case PlayerState.Ready:
                break;

            case PlayerState.NomalFight:
                StopCoroutine("kickBackTimer");
                ShieldBreak = false;

                if (!Attacking)
                {
                    PlayerMove(MoveVecter);
                }
                else
                {
                    PlayerMove(Vector3.zero);
                }

                if (Input.GetKey(KeyCode.LeftShift)) NowPlayerState = PlayerState.LongRange;

                if (Input.GetKey(KeyCode.Space)) NowPlayerState = PlayerState.Shield;

                if (PlayerCurrentLP <= 0f) NowPlayerState = PlayerState.Dead;

                break;

            case PlayerState.LongRange:
                StopCoroutine("kickBackTimer");
                ShieldBreak = false;

                if (!Attacking)
                {
                    LongRangeMove(MoveVecter);
                }
                else
                {
                    LongRangeMove(Vector3.zero);
                }

                if (!Input.GetKey(KeyCode.LeftShift)) NowPlayerState = PlayerState.NomalFight;

                if (Input.GetKey(KeyCode.Space)) NowPlayerState = PlayerState.Shield;

                if (PlayerCurrentLP <= 0f) NowPlayerState = PlayerState.Dead;

                break;

            case PlayerState.KickBack:
                PlayerMove(Vector3.zero);

                break;

            case PlayerState.Shield:
                PlayerMove(MoveVecter / 2);

                if(ShieldCurrentHealth <= 0 && !ShieldBreak)
                {
                    if(PlayerCurrentLP > ShieldCreate)
                    {
                        UsingLP(ShieldCreate);
                        ShieldCurrentHealth = MaxShieldHealth;

                        ShieldModel.GetComponent<Renderer>().material.SetFloat("_ClipingValue", 0);
                    }
                    else
                    {
                        Debug.Log("PLが足りません");
                    }
                }

                if (!Input.GetKey(KeyCode.Space)) NowPlayerState = PlayerState.NomalFight;

                if (PlayerCurrentLP <= 0f) NowPlayerState = PlayerState.Dead;

                break;

            case PlayerState.Dead:

                PlayerMove(Vector3.zero);

                playerRigidbody.isKinematic = true;
                this.GetComponent<Collider>().enabled = false;

                effect.DeadEffect(1);

                break;
        }

        ShieldModel.SetActive(NowPlayerState == PlayerState.Shield && ShieldCurrentHealth > 0.0f);

        playerAnimator.SetInteger("PlayerState", (int)NowPlayerState);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="move"></param>
    void PlayerMove(Vector3 move)
    {
        SetPlayerDir(move);
        IsGrounded = Physics.Raycast(RayOrigin.position, -RayOrigin.up, RayRange);

        if (IsGrounded)
        {
            Quaternion camRotation = Quaternion.Euler(0, CameraTransform.eulerAngles.y, 0);
            playerRigidbody.AddForce(moveMultiply * ((camRotation * move) - playerRigidbody.velocity));
        }
        else
        {
            playerRigidbody.AddForce(Vector3.zero);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="move"></param>
    void LongRangeMove(Vector3 move)
    {
        if (IsGrounded)
        {
            transform.rotation = Quaternion.Euler(0, CameraTransform.eulerAngles.y, 0);

            Vector3 _move = transform.TransformDirection(move / 2);
            playerRigidbody.AddForce(moveMultiply * (_move - playerRigidbody.velocity));
        }
        else
        {
            playerRigidbody.AddForce(Vector3.zero);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dir"></param>
    void SetPlayerDir(Vector3 dir)
    {
        Quaternion camRotation = Quaternion.Euler(0, CameraTransform.eulerAngles.y, 0);

        if (dir.magnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(camRotation * dir),
                TurnSpeed
            );
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="weight"></param>
    public void AttackMove(float weight)
    {
        SetPlayerDir(MoveVecter);
        playerRigidbody.AddForce(transform.forward * AttackJump * weight ,ForceMode.Impulse);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="AttackForce"></param>
    public void Damage(float damage, Vector3 AttackForce)
    {
        Debug.Log("player damage");
        
        if (NowPlayerState == PlayerState.Shield && ShieldCurrentHealth > 0.0f)
        {
            ShieldCurrentHealth -= damage;
            ShieldModel.GetComponent<Renderer>().material.SetFloat("_ClipingValue", 1 - (ShieldCurrentHealth / MaxShieldHealth));

            if (ShieldCurrentHealth <= 0.0f) ShieldBreak = true;
        }
        else if(NowPlayerState != PlayerState.KickBack)
        {
            PlayerCurrentLP -= damage;
            healthBar.SetNowHealth(PlayerCurrentLP / PlayerMaxLP,true);

            if(PlayerCurrentLP <= 0)
            {
                NowPlayerState = PlayerState.Dead;
                PlayerCurrentLP = 0;
                healthBar.SetNowHealth(0, true);
            }
            else if (PlayerCurrentLP <= PlayerMaxLP / 3)
            {
                kickBackDrection = AttackForce;
                StartCoroutine(KickBackTimer(0.5f));
            }
            else
            {
                StartCoroutine(DamageEffect(0.5f));
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="usePoint"></param>
    public void UsingLP(float usePoint)
    {
        PlayerCurrentLP -= usePoint;
        healthBar.SetNowHealth(PlayerCurrentLP / PlayerMaxLP,false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    IEnumerator DamageEffect(float waitTime)
    {
        effect.DamageEffect(1);

        yield return new WaitForSeconds(waitTime);

        effect.DamageEffect(0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="backTime"></param>
    /// <returns></returns>
    IEnumerator KickBackTimer(float backTime)
    {
        NowPlayerState = PlayerState.KickBack;
        KickBackMove(kickBackDrection);

        effect.KickBackEffect(1);

        yield return new WaitForSeconds(backTime);

        NowPlayerState = PlayerState.NomalFight;
        effect.KickBackEffect(0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="move"></param>
    void KickBackMove(Vector3 move)
    {
        transform.rotation = Quaternion.LookRotation(-move,transform.up);

        playerRigidbody.AddForce(move * kickBackStrength,ForceMode.Impulse);
    }
}
