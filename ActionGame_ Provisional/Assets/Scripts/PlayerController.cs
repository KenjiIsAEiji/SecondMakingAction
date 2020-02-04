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
    
    [SerializeField] Transform CameraTransform;
    [SerializeField] Transform BodyTransform;

    public bool Attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        MoveVecter = new Vector3(moveX * Speed, 0, moveZ * Speed);

        if (!Attacking)
        {
            playerRigidbody.AddForce(transform.TransformDirection(MoveVecter));

            if (moveX != 0 || moveZ != 0)
            {
                BodyTransform.localRotation = Quaternion.Slerp(
                    BodyTransform.localRotation,
                    Quaternion.LookRotation(new Vector3(moveX, 0, moveZ)),
                    TurnSpeed * Time.deltaTime
                );
                transform.rotation = Quaternion.Euler(0, CameraTransform.eulerAngles.y, 0);
            }
        }
        else
        {
            playerRigidbody.AddForce(Vector3.zero);
        }

        
    }
}
