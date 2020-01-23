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
        animator = GetComponent<Animator>();

        animator.SetFloat("Speed", characterController.velocity.magnitude);
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        //animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
        //animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);

        //animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandTransform.position);
        //animator.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandTransform.rotation);

        //animator.SetIKPosition(AvatarIKGoal.RightHand, RightHandTransform.position);
        //animator.SetIKRotation(AvatarIKGoal.RightHand, RightHandTransform.rotation);
    }

    void Hit()
    {
        Debug.Log("Hit!!");
    }
    
    void AttackEnter()
    {
        Debug.Log("Attack Start");
        moveController.Attacking = true;
    }

    void AttackExit()
    {
        Debug.Log("Attack End");
        moveController.Attacking = false;
    }
}
