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

    [Header("足音")]
    public AudioClip walksound;
    AudioSource audiosource;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();

        swordCollider.enabled = false;

        NowMotionScale = 0;

        audiosource = GetComponent<AudioSource>();

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

        animator.SetBool("LightAttack", Input.GetMouseButton(0));
        animator.SetBool("HeavyAttack", Input.GetMouseButton(1));

        //if (Input.GetMouseButtonDown(0) && playerController.MoveVecter.magnitude > 1f)
        //{
        //    animator.SetTrigger("Attack");
        //    animator.SetInteger("AttackType", 1);
        //}
        //else if(Input.GetMouseButtonDown(0))
        //{
        //    animator.SetTrigger("Attack");
        //    animator.SetInteger("AttackType", 0);
        //}
        //else if(Input.GetMouseButtonDown(1))
        //{
        //    animator.SetTrigger("Attack");
        //    animator.SetInteger("AttackType", 2);
        //}
    }

    void AttackEnter(float motionScale)
    {
        Debug.Log("Attack Start");
        testEffect.GetComponent<ParticleSystem>().Play();
        swordCollider.enabled = true;

        NowMotionScale = motionScale;
        playerController.AttackMove(motionScale);
    }

    public void AttackExit()
    {
        Debug.Log("Attack End");
        testEffect.GetComponent<ParticleSystem>().Stop();
        swordCollider.enabled = false;

        NowMotionScale = 0;
    }

    void WalksoundEnter()
    {
        if (playerController.IsGrounded == true)
        {
            Debug.Log("Sound Start");
            audiosource.PlayOneShot(walksound);
        }
    }

    //void WalksoundExit()
    //{
    //    if (playerController.IsGrounded == false)
    //    {
    //        Debug.Log("Sound End");
    //        audiosource.Stop();
    //    }
    //}
}
