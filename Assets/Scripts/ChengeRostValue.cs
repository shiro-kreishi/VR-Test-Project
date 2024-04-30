using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChengeRostValue : MonoBehaviour
{
    public IkAvatarScript script;
    public TMP_InputField Input;
    private float kek;

    public void TestChengeFunc()
    {
        // Попробуйте преобразовать текст в число с плавающей точкой
        if (float.TryParse(Input.text, out float result))
        {
            // Если это удается, передайте значение в метод
            script.ChengeRost(result);
        }
        else
        {
            // Если нет, то возможно хотите обработать ошибку преобразования
            Debug.LogError("Невозможно преобразовать введенную строку в число с плавающей точкой.");
        }
    }
}