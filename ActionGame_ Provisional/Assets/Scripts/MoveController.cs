using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MoveController : MonoBehaviour
{
    CharacterController characterController;
    Vector3 MoveVector = Vector3.zero;

    [SerializeField] float MoveSpeed = 10.0f;
    [SerializeField] float Gravity = 10.0f;

    [SerializeField] float turnSpeed = 20.0f;

    [SerializeField] Transform CameraTransform;
    [SerializeField] Transform BodyTransform;

    public bool Attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (characterController.isGrounded)
        {
            MoveVector = new Vector3(moveX * MoveSpeed, 0, moveZ * MoveSpeed);
        }

        MoveVector.y -= Gravity * Time.deltaTime;

        if(Attacking == false)
        {
            characterController.Move(transform.TransformDirection(MoveVector * Time.deltaTime));

            if (moveX != 0 || moveZ != 0)
            {
                BodyTransform.localRotation = Quaternion.Slerp(
                    BodyTransform.localRotation,
                    Quaternion.LookRotation(new Vector3(moveX, 0, moveZ)),
                    turnSpeed * Time.deltaTime
                );
                transform.rotation = Quaternion.Euler(0, CameraTransform.eulerAngles.y, 0);
            }
        }
        else
        {
            characterController.Move(transform.TransformDirection(MoveVector * 0));
        }
    }
}
