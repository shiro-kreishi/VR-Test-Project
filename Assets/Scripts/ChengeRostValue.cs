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
        // ���������� ������������� ����� � ����� � ��������� ������
        if (float.TryParse(Input.text, out float result))
        {
            // ���� ��� �������, ��������� �������� � �����
            script.ChengeRost(result);
        }
        else
        {
            // ���� ���, �� �������� ������ ���������� ������ ��������������
            Debug.LogError("���������� ������������� ��������� ������ � ����� � ��������� ������.");
        }
    }
}