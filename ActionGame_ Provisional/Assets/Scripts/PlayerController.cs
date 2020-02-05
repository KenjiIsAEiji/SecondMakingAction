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
    
    [SerializeField] Transform CameraTransform;

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
            Quaternion camRotation = Quaternion.Euler(0, CameraTransform.eulerAngles.y, 0);
            
            if(MoveVecter.magnitude > 0)
            {
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(camRotation * MoveVecter),
                    TurnSpeed
                );
            }

            playerRigidbody.AddForce(moveMultiply * ((camRotation * MoveVecter) - playerRigidbody.velocity));
        }
        else
        {
            playerRigidbody.AddForce(Vector3.zero);
        }
    }
}
