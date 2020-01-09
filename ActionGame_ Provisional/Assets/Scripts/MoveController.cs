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
            MoveVector.x = moveX * MoveSpeed;
            MoveVector.z = moveZ * MoveSpeed;
        }

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.LookRotation(new Vector3(moveX, 0, moveZ)),
            turnSpeed * Time.deltaTime
        );

        MoveVector.y -= Gravity * Time.deltaTime;

        characterController.Move(MoveVector * Time.deltaTime);
    }
}
