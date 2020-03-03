using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    Rigidbody playerRigidbody;
    Vector3 MoveVecter = Vector3.zero;

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

        switch (NowPlayerState)
        {
            case PlayerState.Ready:
                break;

            case PlayerState.NomalFight:
                StopCoroutine("kickBackTimer");

                if (!Attacking)
                {
                    MoveVecter = new Vector3(InputX * Speed, 0, InputZ * Speed);
                }
                else
                {
                    MoveVecter = Vector3.zero;
                }

                NomalMove(MoveVecter);

                break;

            case PlayerState.LongRange:
                break;

            case PlayerState.KickBack:

                NomalMove(Vector3.zero);

                break;

            case PlayerState.Dead:
                break;
        }

        playerAnimator.SetInteger("PlayerState", (int)NowPlayerState);
    }

    void NomalMove(Vector3 move)
    {
        Quaternion camRotation = Quaternion.Euler(0, CameraTransform.eulerAngles.y, 0);

        if (move.magnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(camRotation * move),
                TurnSpeed
            );
        }

        IsGrounded = Physics.Raycast(RayOrigin.position, -RayOrigin.up, RayRange);

        if (IsGrounded)
        {
            playerRigidbody.AddForce(moveMultiply * ((camRotation * move) - playerRigidbody.velocity));
        }
        else
        {
            playerRigidbody.AddForce(Vector3.zero);
        }
    }

    public void Damage(float damage)
    {
        PlayerCurrentHealth -= damage;
        healthBar.SetNowHealth(PlayerCurrentHealth);
        StartCoroutine(kickBackTimer(0.5f));
    }

    IEnumerator kickBackTimer(float backTime)
    {
        NowPlayerState = PlayerState.KickBack;

        yield return new WaitForSeconds(backTime);

        NowPlayerState = PlayerState.NomalFight;
    }
}
