using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAnimationController : MonoBehaviour
{
    [Range(0f, 1f)]
    public float smoothing = 0.3f;

    [Range(0f, 1f)] // Добавьте настройку порогового значения скорости в инспекторе
    public float speedThreshold = 0.3f; // Пороговое значение по умолчанию установлено как 0.3f

    private Animator animator;
    private Vector3 previousPos;
    private IkAvatarScript vrRig;
    private Vector3 prevForward;

    void Start()
    {
        animator = GetComponent<Animator>();
        vrRig = GetComponent<IkAvatarScript>();
        previousPos = vrRig.head.vrTarget.position;
        prevForward = transform.forward; // Инициализация первоначального направления вперед
    }

    void Update()
    {
        Vector3 headsetSpeed = (vrRig.head.vrTarget.position - previousPos) / Time.deltaTime;
        headsetSpeed.y = 0;
        Vector3 headsetLocalSpeed = transform.InverseTransformDirection(headsetSpeed);
        float currentSpeedMagnitude = headsetLocalSpeed.magnitude; // Вычисляем величину скорости
        previousPos = vrRig.head.vrTarget.position;

        if (currentSpeedMagnitude > speedThreshold) // Проверяем, превышает ли скорость пороговое значение
        {
            // Вычисляем угол между текущим направлением вперед и предыдущим
            float angle = Vector3.SignedAngle(prevForward, transform.forward, Vector3.up);
            prevForward = transform.forward; // Обновляем предыдущее направление вперед

            // Определяем новые направления движения с учетом поворота
            Vector2 rotatedDirection = RotateVector(new Vector2(headsetLocalSpeed.x, headsetLocalSpeed.z), angle);

            // Получаем предыдущие значения для смешивания
            float previousDirectionX = animator.GetFloat("DirectionX");
            float previousDirectionY = animator.GetFloat("DirectionY");

            // Устанавливаем новые значения для параметров анимации
            animator.SetBool("isMoving", true);
            animator.SetFloat("DirectionX", Mathf.Lerp(previousDirectionX, Mathf.Clamp(rotatedDirection.x, -1, 1), smoothing));
            animator.SetFloat("DirectionY", Mathf.Lerp(previousDirectionY, Mathf.Clamp(rotatedDirection.y, -1, 1), smoothing));
        }
        else
        {
            animator.SetBool("isMoving", false); // Если скорость меньше порогового значения, персонаж стоит на месте
        }
    }

    // Вспомогательная функция для вращения вектора
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