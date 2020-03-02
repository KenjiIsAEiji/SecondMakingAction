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

    public enum PlayerState
    {
        Ready = 0,
        NomalFight = 1,
        LongRange = 2,
        Dead = 3
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
        IsGrounded = false;

        NowPlayerState = PlayerState.NomalFight;
        PlayerCurrentHealth = PlayerMaxHealth;
        healthBar.SetMaxHealth(PlayerCurrentHealth);
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

            case PlayerState.Dead:
                break;
        }
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
}
