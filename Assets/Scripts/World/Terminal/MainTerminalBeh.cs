using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
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
    MoveController moveController;
    GameObject textSaved;

    public async UniTask Interact()
    {
        if(moveController == null)
            moveController = FindObjectOfType<MoveController>();
        moveController.enabled = false;

        virtualCamera.gameObject.SetActive(true);
        ScreensOn();
        if (character == null)
            character = FindObjectOfType<Character>();
        if (mainTerminalMenu == null)
            mainTerminalMenu = GameObject.Find("MainTerminalMenu"); // �� ��������� ����� ����������� ������

        int delayBeforeActivateMenu = 1750;
        await UniTask.Delay(delayBeforeActivateMenu);

        if (character.GetCharacterForm() == FormType.Base)
        {
            Debug.Log("Terminal Interact");
            mainTerminalMenu.transform.DOScale(1, 0.3f);
            StepOne();
            MMButtonsBeh.OnExitMainTerminalButtonPushed.AddListener(() => CloseTerminal().Forget());
            await ShowSavedMessege(1000);
        }
        else
        {
            character.gameObject.GetComponent<HintController>().HintTakeTheFormOfSwarm();
        } 
    }

    private async Task ShowSavedMessege(int delayBeforeActivateMenu)
    {
        await UniTask.Delay(delayBeforeActivateMenu);
        if (textSaved == null)
            textSaved = GameObject.Find("TextSaved");
        textSaved.GetComponent<TextMeshProUGUI>().DOFade(1, 2f);
        await UniTask.Delay(delayBeforeActivateMenu);
        textSaved.GetComponent<TextMeshProUGUI>().DOFade(0, 2f);
    }

    public async UniTask CloseTerminal()
    {
        Debug.Log("CloseTerminal");
        ScreensOff();
        virtualCamera.gameObject.SetActive(false);
        MMButtonsBeh.OnExitMainTerminalButtonPushed.RemoveListener(() => CloseTerminal().Forget());
        moveController.enabled = true;
    }

    private void ScreensOn()
    {
        terminalMenu.DOFade(1, 0.5f);
        map.DOFade(1, 0.5f);
    }

    private void ScreensOff()
    {
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
    /// 

    private void OnDisable()
    {
        MMButtonsBeh.OnExitMainTerminalButtonPushed.RemoveListener(() => CloseTerminal().Forget());
    }
}