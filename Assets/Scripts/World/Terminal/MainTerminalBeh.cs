using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class MainTerminalBeh : MonoBehaviour, IInteractableTerminal
{
    [SerializeField] int terminalNumber; // �������� � �������
    [SerializeField] SpriteRenderer terminalMenu;
    [SerializeField] SpriteRenderer map;
    [SerializeField] GameObject virtualCamera;

    Character character;
    GameObject mainTerminalMenu;

    public async UniTask Interact()
    {
        virtualCamera.gameObject.SetActive(true);
        ScreensOn();
        if (character == null)
            character = FindObjectOfType<Character>();
        if (mainTerminalMenu == null)
            mainTerminalMenu = GameObject.Find("MainTerminalMenu"); // �� ��������� ����� ����������� ������

        int delayBeforeActivateMenu = 1500;
        await UniTask.Delay(delayBeforeActivateMenu);

        if (character.GetCharacterForm() == FormType.Base)
        {
            Debug.Log("Terminal Interact");
            mainTerminalMenu.transform.DOScale(1, 0.3f);
            StepOne();
        }
        else
        {
            character.gameObject.GetComponent<HintController>().HintTakeTheFormOfSwarm();
        }

        MMButtonsBeh.ExitButtonPushed.AddListener(() => CloseTerminal().Forget());
    }

    public async UniTask CloseTerminal()
    {
        ScreensOff();
        virtualCamera.gameObject.SetActive(true);
    }

    private void ScreensOn()
    {
        terminalMenu.DOFade(1, 0.5f);
        map.DOFade(1, 0.5f);
    }

    private void ScreensOff()
    {
        mainTerminalMenu.SetActive(false);
        terminalMenu.DOFade(0, 0f);
        map.DOFade(0, 0f);
        mainTerminalMenu.transform.DOScale(0, 0f);
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