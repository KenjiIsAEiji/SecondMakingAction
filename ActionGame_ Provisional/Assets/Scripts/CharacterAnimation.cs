using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    Animator animator;
    [SerializeField] CharacterController characterController;

    [SerializeField] Transform LeftHandTransform;
    [SerializeField] Transform RightHandTransform;

    [SerializeField] MoveController moveController;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("MoveTree"))
        {
            moveController.Attacking = false;
        }
        else
        {
            moveController.Attacking = true;
        }

        animator.SetFloat("Speed", characterController.velocity.magnitude);
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
            animator.SetInteger("AttackType", 0);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Attack");
            animator.SetInteger("AttackType", 1);
        }
        
    }

    void Hit()
    {
        Debug.Log("Hit!!");
    }
}
