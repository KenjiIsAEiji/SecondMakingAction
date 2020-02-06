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

    [Header("接地判定用のRay発生位置")]
    [SerializeField] Transform RayOrigin;
    [SerializeField] float RayRange = 10;

    public bool Attacking = false;
    public bool IsGrounded;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        IsGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Attacking)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            MoveVecter = new Vector3(moveX * Speed, 0, moveZ * Speed);
        }
    }

    private void FixedUpdate()
    {
        Quaternion camRotation = Quaternion.Euler(0, CameraTransform.eulerAngles.y, 0);

        if (MoveVecter.magnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(camRotation * MoveVecter),
                TurnSpeed
            );
        }

        if (Physics.Raycast(RayOrigin.position, -RayOrigin.up, RayRange))
        {
            playerRigidbody.AddForce(moveMultiply * ((camRotation * MoveVecter) - playerRigidbody.velocity));
        }
        else
        {
            playerRigidbody.AddForce(Vector3.zero);
        }
    }
}
