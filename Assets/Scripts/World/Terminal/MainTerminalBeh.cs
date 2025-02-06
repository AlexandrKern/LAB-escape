using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MainTerminalBeh : MonoBehaviour, IInteractableTerminal
{
    [SerializeField] int terminalNumber; // нумеруем с единицы
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
            mainTerminalMenu = GameObject.Find("MainTerminalMenu"); // не получится найти выключенный объект

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
            //"если у героя полное здоровье, то идёт автоматическое сохранение прогресса" (из ТЗ)
            // "а также теряется 1 частица из облака (перетекает в терминал)" (из ТЗ).
            // Тут пока не ясно как эти частицы перетикают". Оставим на потом.
        }
        else
        {
            character.gameObject.GetComponent<CharacterHealth>().ResetHealth();
            // сначала происходит полное восстановление всех частиц (отхил, частицы текут из терминала к герою) (из ТЗ)
            // и только после отхила идёт автоматическое сохранение прогресса, а также теряется 1 частица из облака (из ТЗ)x
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
            // если это первое посещение данного терминала, то игроку сначала выдаётся кусок лора/сюжета игры и только после этого экран
            // терминала увеличивается и приближается (наезд камеры) и игроку предоставляется выбор из нескольких опций (см. STEP 3);
            Debug.Log("показан лор");
            DataTerminals.SetTerminalAvailability(terminalNumber, true);
            DataTerminals.SaveData();
        }
        else
        {
            // если это не первое посещение данного терминала, то экран терминала сразу увеличивается и приближается (наезд камеры)
            // и игроку предоставляется выбор из нескольких опций
        }
    }


    /// <summary>
    /// следующие методы повесим на UI
    /// </summary>
    public void ShowMap()
    {
        // пока нет нарисованной карты
    }

    public void ShowLore()
    {
        // пока нет лора
    }

    public void ShowCollectable()
    {
        // пока нет Collectables
    }

    /// <summary>
    /// вышеуказанные методы вешаем на UI
    /// </summary>
    /// 

    private void OnDisable()
    {
        MMButtonsBeh.OnExitMainTerminalButtonPushed.RemoveListener(() => CloseTerminal().Forget());
    }
}