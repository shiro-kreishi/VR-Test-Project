using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class VRFootIK : MonoBehaviour
{
    private Animator animator;
    public Vector3 footOffset;

    [Range(0f, 1f)]
    public float rightFootPosWeight = 1;
    [Range(0f, 1f)]
    public float leftFootPosWeight = 1;
    [Range(0f, 1f)]
    public float rightFootRotWeight = 1;
    [Range(0f, 1f)]
    public float leftFootRotWeight = 1;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        RaycastHit rightHit;
        RaycastHit leftHit;
        Vector3 rightFootPos = animator.GetIKPosition(AvatarIKGoal.RightFoot);
        Vector3 leftFootPos = animator.GetIKPosition(AvatarIKGoal.LeftFoot);

        bool rightHasHit = Physics.Raycast(rightFootPos + Vector3.up, Vector3.down, out rightHit);
        bool leftHasHit = Physics.Raycast(leftFootPos + Vector3.up, Vector3.down, out leftHit);

        if (rightHasHit)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPosWeight);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, rightHit.point + footOffset);

            Quaternion footRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, rightHit.normal), rightHit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.RightFoot, footRotation);
        }
        else
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);

        if (leftHasHit)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootPosWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftHit.point + footOffset);

            Quaternion leftfootRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, leftHit.normal), leftHit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftfootRotation);
        }
        else
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
    }
}
