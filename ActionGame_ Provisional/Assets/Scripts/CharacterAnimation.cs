using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    Animator animator;
    [SerializeField] CharacterController characterController;

    [SerializeField] MoveController moveController;

    [Header("デバッグ用エフェクト")]
    [SerializeField] GameObject testEffect;

    [Header("剣のコライダー")]
    [SerializeField] Collider swordCollider;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        swordCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("MoveTree"))
        {
            moveController.Attacking = false;
            animator.SetFloat("Speed", characterController.velocity.magnitude);
        }
        else
        {
            moveController.Attacking = true;
            animator.SetFloat("Speed", 0);
        }
        
        if (Input.GetMouseButtonDown(0) && characterController.velocity.magnitude > 0)
        {
            animator.SetTrigger("Attack");
            animator.SetInteger("AttackType", 1);
        }
        else if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
            animator.SetInteger("AttackType", 0);
        }
        else if(Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Attack");
            animator.SetInteger("AttackType", 2);
        }
    }

    void AttackEnter()
    {
        Debug.Log("Attack Start");
        testEffect.GetComponent<ParticleSystem>().Play();
        swordCollider.enabled = true;
    }

    void AttackExit()
    {
        Debug.Log("Attack End");
        testEffect.GetComponent<ParticleSystem>().Stop();
        swordCollider.enabled = false;
    }
}
