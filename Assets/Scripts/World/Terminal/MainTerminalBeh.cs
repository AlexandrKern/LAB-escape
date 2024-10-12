using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class MainTerminalBeh : MonoBehaviour, IInteractableTerminal
{
    [SerializeField] int terminalNumber; // нумеруем таки с единицы
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
}