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
            else if (typeOfHint == TypeOfHint.hammerPress2)
            {
                collision.GetComponent<HintController>().HintHammerPress2();
            }
            else if (typeOfHint == TypeOfHint.canBeDestroyed)
            {
                collision.GetComponent<HintController>().HintCanBeCrushed();
            }
            else if (typeOfHint == TypeOfHint.columntNotStrong)
            {
                collision.GetComponent<HintController>().ColumnCanBeCrushed();
            }
            else if (typeOfHint == TypeOfHint.columntNotStrong)
            {
                collision.GetComponent<HintController>().RealGameStartHere();
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
        hammer,
        hammerPress2,
        canBeDestroyed,
        columntNotStrong,
        realGameStartHere,
    }
}
