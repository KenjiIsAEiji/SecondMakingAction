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

    public bool Attacking = false;
    public bool IsGrounded;

    Animator playerAnimator;

    public enum PlayerState
    {
        Ready = 0,
        NomalFight = 1,
        LongRange = 2,
        KickBack = 3,
        Dead = 4
    }
    [Header("- Playerの状態 -")]
    public PlayerState NowPlayerState;

    [Header("- PlayerのHP -")]
    public float PlayerMaxHealth = 100f;
    public float PlayerCurrentHealth;
    [SerializeField] HealthBar healthBar;

    [Header("- ノックバック -")]
    [SerializeField] float kickBackStrength = 2f;
    private Vector3 kickBackDrection;
    [SerializeField] CamEffect effect;

    [Header("- 攻撃時の移動 -")]
    [SerializeField] float AttackJump = 20f;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        IsGrounded = false;

        NowPlayerState = PlayerState.NomalFight;
        PlayerCurrentHealth = PlayerMaxHealth;

        healthBar.SetMaxHealth(PlayerMaxHealth);
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

                if (!Attacking)
                {
                    PlayerMove(MoveVecter);
                }
                else
                {
                    PlayerMove(Vector3.zero);
                }

                if (Input.GetKey(KeyCode.LeftShift)) NowPlayerState = PlayerState.LongRange;

                break;

            case PlayerState.LongRange:
                StopCoroutine("kickBackTimer");

                
                if (!Attacking)
                {
                    LongRangeMove(MoveVecter);
                }
                else
                {
                    LongRangeMove(Vector3.zero);
                }

                if (!Input.GetKey(KeyCode.LeftShift)) NowPlayerState = PlayerState.NomalFight;

                break;

            case PlayerState.KickBack:

                PlayerMove(Vector3.zero);

                break;

            case PlayerState.Dead:
                break;
        }

        playerAnimator.SetInteger("PlayerState", (int)NowPlayerState);
    }

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

    public void AttackMove(float weight)
    {
        SetPlayerDir(MoveVecter);
        playerRigidbody.AddForce(transform.forward * AttackJump * weight ,ForceMode.Impulse);
    }

    public void Damage(float damage, Vector3 AttackForce)
    {
        Debug.Log("player damage");
        if (NowPlayerState != PlayerState.KickBack)
        {
            PlayerCurrentHealth -= damage;
            healthBar.SetNowHealth(PlayerCurrentHealth);
        }

        if (PlayerCurrentHealth <= PlayerMaxHealth / 3)
        {
            kickBackDrection = AttackForce;
            StartCoroutine(KickBackTimer(0.5f));
        }
        else
        {
            StartCoroutine(DamageEffect(0.5f));
        }
    }

    IEnumerator DamageEffect(float waitTime)
    {
        effect.DamageEffect(1);

        yield return new WaitForSeconds(waitTime);

        effect.DamageEffect(0);
    }

    IEnumerator KickBackTimer(float backTime)
    {
        NowPlayerState = PlayerState.KickBack;
        KickBackMove(kickBackDrection);

        effect.KickBackEffect(1);

        yield return new WaitForSeconds(backTime);

        NowPlayerState = PlayerState.NomalFight;
        effect.KickBackEffect(0);
    }

    void KickBackMove(Vector3 move)
    {
        transform.rotation = Quaternion.LookRotation(-move,transform.up);

        playerRigidbody.AddForce(move * kickBackStrength,ForceMode.Impulse);
    }
}
