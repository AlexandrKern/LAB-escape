using UnityEngine;

public class MainTerminalBeh : MonoBehaviour
{
    [SerializeField] int TerminalNumber; // ��������� ��������� ���������� (��� �� ���������) � ����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StepOne();
        }
    }

    private void StepOne()
    {
        Data.CheckpointNumber = TerminalNumber;
        if (Data.HP == Data.FullHP)
        {
            Data.SaveData(); //"���� � ����� ������ ��������, �� ��� �������������� ���������� ���������" (�� ��)
            // "� ����� �������� 1 ������� �� ������ (���������� � ��������)" (�� ��).
            // ��� ���� �� ���� ��� ��� ������� ����������". ������� �� �����.
            StepTwo();
        }
        else
        {
            // ������� ���������� ������ �������������� ���� ������ (�����, ������� ����� �� ��������� � �����) (�� ��)
            Data.HP = Data.FullHP;
            // � ������ ����� ������ ��� �������������� ���������� ���������, � ����� �������� 1 ������� �� ������ (�� ��)
            Data.SaveData();
            StepTwo();
        }
    }

    private void StepTwo()
    {
        if (DataTerminals.IsTerminalFirstTimeVisit(TerminalNumber))
        {
            // ���� ��� ������ ��������� ������� ���������, �� ������ ������� ������� ����� ����/������ ���� � ������ ����� ����� �����
            // ��������� ������������� � ������������ (����� ������) � ������ ��������������� ����� �� ���������� ����� (��. STEP 3);
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