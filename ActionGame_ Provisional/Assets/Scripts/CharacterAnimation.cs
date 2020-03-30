using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    Animator animator;
    Rigidbody playerRigidbody;
    PlayerController playerController;

    [Header("攻撃時のエフェクト")]
    [SerializeField] Transform SponeOrigin;
    [SerializeField] GameObject AttackEffect;

    [Header("遠距離攻撃")]
    [SerializeField] GameObject SlashPrefab;
    [SerializeField] float SlashSpeed = 20f;
    [SerializeField] float SlashUseLP = 10f;

    [Header("剣のコライダー")]
    [SerializeField] Collider swordCollider;

    public float NowMotionScale = 0;

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

        audiosource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("MoveTree"))
        {
            playerController.Attacking = false;
            animator.SetFloat("SpeedX", playerRigidbody.velocity.magnitude);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("LongRange Move Tree"))
        {
            playerController.Attacking = false;
            animator.SetFloat("SpeedX", playerController.MoveVecter.x);
            animator.SetFloat("SpeedZ", playerController.MoveVecter.z);
        }
        else
        {
            playerController.Attacking = true;
            animator.SetFloat("SpeedX", 0);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetLayerWeight(animator.GetLayerIndex("Shild Layer"), 1);
            animator.SetBool("LightAttack", false);
            animator.SetBool("HeavyAttack", false);
        }
        else
        {
            animator.SetLayerWeight(animator.GetLayerIndex("Shild Layer"), 0);
            animator.SetBool("LightAttack", Input.GetMouseButton(0));
            animator.SetBool("HeavyAttack", Input.GetMouseButton(1));
        }

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
        //testEffect.GetComponent<ParticleSystem>().Play();
        
        swordCollider.enabled = true;
        NowMotionScale = motionScale;

        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("LongRangeMode"))
        {
            GameObject SlashObj = Instantiate(SlashPrefab, SponeOrigin.position, Quaternion.LookRotation(transform.forward));
            SlashObj.GetComponent<Rigidbody>().AddForce(transform.forward * SlashSpeed,ForceMode.Impulse);

            playerController.UsingLP(SlashUseLP);

            Destroy(SlashObj, 5f);
        }
        else
        {
            playerController.AttackMove(NowMotionScale);
            GameObject obj = Instantiate(AttackEffect, SponeOrigin.position, swordCollider.gameObject.transform.rotation);
            obj.transform.parent = this.transform;
        }
    }

    public void AttackExit()
    {
        Debug.Log("Attack End");
        //testEffect.GetComponent<ParticleSystem>().Stop();
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
