using UnityEngine;

public class MainTerminalBeh : MonoBehaviour, IInteractableTerminal
{
    [SerializeField] int TerminalNumber; // начинайте нумерацию терминалов (они же чекпоинты) с нуля

    public void Interact()
    {
        StepOne();
        Debug.Log("Terminal Interact");
    }

    private void StepOne()
    {
        Data.CheckpointNumber = TerminalNumber;
        if (Data.HP == Data.FullHP)
        {
            Data.SaveData(); //"если у героя полное здоровье, то идёт автоматическое сохранение прогресса" (из ТЗ)
            // "а также теряется 1 частица из облака (перетекает в терминал)" (из ТЗ).
            // Тут пока не ясно как эти частицы перетикают". Оставим на потом.
            StepTwo();
        }
        else
        {
            // сначала происходит полное восстановление всех частиц (отхил, частицы текут из терминала к герою) (из ТЗ)
            Data.HP = Data.FullHP;
            // и только после отхила идёт автоматическое сохранение прогресса, а также теряется 1 частица из облака (из ТЗ)
            Data.SaveData();
            StepTwo();
        }
    }

    private void StepTwo()
    {
        if (DataTerminals.IsTerminalFirstTimeVisit(TerminalNumber))
        {
            // если это первое посещение данного терминала, то игроку сначала выдаётся кусок лора/сюжета игры и только после этого экран
            // терминала увеличивается и приближается (наезд камеры) и игроку предоставляется выбор из нескольких опций (см. STEP 3);
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