using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class MainTerminalBeh : MonoBehaviour, IInteractableTerminal
{
    [SerializeField] int terminalNumber; // �������� ���� � �������
    [SerializeField] GameObject terminalMenu;
    Character character;
    public async UniTask Interact()
    {
        if(character == null)
        character = FindObjectOfType<Character>();
        if (character.GetCharacterForm() == FormType.Base)
        {
            Debug.Log("Terminal Interact");
            StepOne();
        }
        else
        {
            character.gameObject.GetComponent<HintController>().HintTakeTheFormOfSwarm();
        }
    }

    private void StepOne()
    {
        Debug.Log("StepOne");
        Data.CheckpointNumber = terminalNumber;
        if (Data.HP == Data.FullHP)
        {
            //"���� � ����� ������ ��������, �� ��� �������������� ���������� ���������" (�� ��)
            // "� ����� �������� 1 ������� �� ������ (���������� � ��������)" (�� ��).
            // ��� ���� �� ���� ��� ��� ������� ����������". ������� �� �����.
        }
        else
        {
            character.gameObject.GetComponent<CharacterHealth>().ResetHealth();
            // ������� ���������� ������ �������������� ���� ������ (�����, ������� ����� �� ��������� � �����) (�� ��)
            // � ������ ����� ������ ��� �������������� ���������� ���������, � ����� �������� 1 ������� �� ������ (�� ��)x
        }
        Data.SaveData();
        DataItem.SaveData();
        StepTwo();
    }

    private void StepTwo()
    {
        Debug.Log("StepTwo");
        if (DataTerminals.IsTerminalFirstTimeVisit(terminalNumber))
        {
            // ���� ��� ������ ��������� ������� ���������, �� ������ ������� ������� ����� ����/������ ���� � ������ ����� ����� �����
            // ��������� ������������� � ������������ (����� ������) � ������ ��������������� ����� �� ���������� ����� (��. STEP 3);
            Debug.Log("������� ���");
            DataTerminals.SetTerminalAvailability(terminalNumber, true);
            DataTerminals.SaveData();
        }
        else
        {
            // ���� ��� �� ������ ��������� ������� ���������, �� ����� ��������� ����� ������������� � ������������ (����� ������)
            // � ������ ��������������� ����� �� ���������� �����
        }
    }


    /// <summary>
    /// ��������� ������ ������� �� UI
    /// </summary>
    public void ShowMap()
    {
        // ���� ��� ������������ �����
    }

    public void ShowLore()
    {
        // ���� ��� ����
    }

    public void ShowCollectable()
    {
        // ���� ��� Collectables
    }

    /// <summary>
    /// ������������� ������ ������ �� UI
    /// </summary>
}