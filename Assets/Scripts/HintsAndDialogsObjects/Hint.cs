using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnHint : MonoBehaviour
{
    [SerializeField] TypeOfHint typeOfHint;
    [SerializeField] string hintKey; // �� ������ ��������� ���� �� ��������� ����� ���������� 

    private void Start()
    {
        if (!string.IsNullOrEmpty(hintKey) && PlayerPrefs.HasKey(hintKey))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(typeOfHint == TypeOfHint.columnHint)
            {
                if (collision.GetComponent<Character>().GetCharacterForm() == FormType.Base)
                {
                    collision.GetComponent<HintController>().HintPressQForObstacle();
                }
                else
                {
                    collision.GetComponent<HintController>().HintTakeTheFormOfSwarm();
                }
            }
            else if (typeOfHint == TypeOfHint.ventilationHint)
            {
                if (collision.GetComponent<Character>().GetCharacterForm() == FormType.Base)
                {
                    collision.GetComponent<HintController>().HintPressQForVentilation();
                }
                else
                {
                    collision.GetComponent<HintController>().HintTakeTheFormOfSwarm();
                }
            }
            else if (typeOfHint == TypeOfHint.hammer)
            {
                collision.GetComponent<HintController>().HintHammerFormEnabled();
            }
        }
    }

    private void OnDestroy()
    {
        // ������ �� ���������� ���������, ������� ��� ���������� � ���������� - ��������� � ������ ���� � ������ �� ����������
        // ��� ����� ������������ ������, ������� � ����� ��� �� ����� ��������� ��������������
        if (!string.IsNullOrEmpty(hintKey) && !PlayerPrefs.HasKey(hintKey))
        {
            PlayerPrefs.SetString(hintKey, "shown");
            PlayerPrefs.Save();
        }
    }

    enum TypeOfHint
    {
        columnHint,
        ventilationHint,
        hammer
    }
}
