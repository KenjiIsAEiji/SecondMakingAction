using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    Animator animator;
    Rigidbody playerRigidbody;
    PlayerController playerController;

    [Header("デバッグ用エフェクト")]
    [SerializeField] GameObject testEffect;

    [Header("剣のコライダー")]
    [SerializeField] Collider swordCollider;

    public float NowMotionScale;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();

        swordCollider.enabled = false;
        NowMotionScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("MoveTree"))
        {
            playerController.Attacking = false;
            animator.SetFloat("Speed", playerRigidbody.velocity.magnitude);
        }
        else
        {
            playerController.Attacking = true;
            animator.SetFloat("Speed", 0);
        }
        
        if (Input.GetMouseButtonDown(0) && playerRigidbody.velocity.magnitude > 0.1f)
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

    void AttackEnter(float motionScale)
    {
        //Debug.Log("Attack Start");
        testEffect.GetComponent<ParticleSystem>().Play();
        swordCollider.enabled = true;

        NowMotionScale = motionScale;
    }

    void AttackExit()
    {
        //Debug.Log("Attack End");
        testEffect.GetComponent<ParticleSystem>().Stop();
        swordCollider.enabled = false;

        NowMotionScale = 0;
    }
}
