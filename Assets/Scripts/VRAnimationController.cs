using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAnimationController : MonoBehaviour
{
    [Range(0f, 1f)]
    public float smoothing = 0.3f;

    [Range(0f, 1f)] // �������� ��������� ���������� �������� �������� � ����������
    public float speedThreshold = 0.3f; // ��������� �������� �� ��������� ����������� ��� 0.3f

    private Animator animator;
    private Vector3 previousPos;
    private IkAvatarScript vrRig;
    private Vector3 prevForward;

    void Start()
    {
        animator = GetComponent<Animator>();
        vrRig = GetComponent<IkAvatarScript>();
        previousPos = vrRig.head.vrTarget.position;
        prevForward = transform.forward; // ������������� ��������������� ����������� ������
    }

    void Update()
    {
        Vector3 headsetSpeed = (vrRig.head.vrTarget.position - previousPos) / Time.deltaTime;
        headsetSpeed.y = 0;
        Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
        float currentSpeedMagnitude = headsetLocalSpeed.magnitude; // ��������� �������� ��������
        previousPos = vrRig.head.vrTarget.position;

        if (currentSpeedMagnitude > speedThreshold) // ���������, ��������� �� �������� ��������� ��������
        {
            // ��������� ���� ����� ������� ������������ ������ � ����������
            float angle = Vector3.SignedAngle(prevForward, transform.forward, Vector3.up);
            prevForward = transform.forward; // ��������� ���������� ����������� ������

            // ���������� ����� ����������� �������� � ������ ��������
            Vector2 rotatedDirection = RotateVector(new Vector2(headsetLocalSpeed.x, headsetLocalSpeed.z), angle);

            // �������� ���������� �������� ��� ����������
            float previousDirectionX = animator.GetFloat("DirectionX");
            float previousDirectionY = animator.GetFloat("DirectionY");

            // ������������� ����� �������� ��� ���������� ��������
            animator.SetBool("isMoving", true);
            animator.SetFloat("DirectionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(rotatedDirection.x, -1, 1), smoothing));
            animator.SetFloat("DirectionY", Mathf.Lerp(previousDirectionY, Mathf.Clamp(rotatedDirection.y, -1, 1), smoothing));
        }
        else
        {
            animator.SetBool("isMoving", false); // ���� �������� ������ ���������� ��������, �������� ����� �� �����
        }
    }

    // ��������������� ������� ��� �������� �������
    private Vector2 RotateVector(Vector2 v, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radian);
        float cos = Mathf.Cos(radian);

        float tx = v.x;
        float ty = v.y;

        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);

        return v;
    }
}