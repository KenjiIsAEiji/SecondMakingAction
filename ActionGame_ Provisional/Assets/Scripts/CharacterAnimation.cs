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
        }
    }

    void Hit()
    {
        Debug.Log("Hit!!");
    }
    
    void AttackEnter()
    {
        Debug.Log("Attack Start");
    }

    void AttackExit()
    {
        Debug.Log("Attack End");
    }
}
