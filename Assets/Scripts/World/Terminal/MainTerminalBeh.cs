using Cysharp.Threading.Tasks;
using UnityEngine;

public class MainTerminalBeh : MonoBehaviour, IInteractableTerminal
{
    [SerializeField] int terminalNumber; // �������� ���� � �������
    [SerializeField] GameObject terminalMenu;

    public async UniTask Interact()
    {
        Debug.Log("Terminal Interact");
        StepOne();
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
            Data.HP = Data.FullHP; 
            // ������� ���������� ������ �������������� ���� ������ (�����, ������� ����� �� ��������� � �����) (�� ��)
            // � ������ ����� ������ ��� �������������� ���������� ���������, � ����� �������� 1 ������� �� ������ (�� ��)x
        }
        Data.SaveData();
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