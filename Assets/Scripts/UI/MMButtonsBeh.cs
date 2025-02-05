using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MMButtonsBeh : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Button button;
    [SerializeField] ButtonsType buttonsNumber;
    [SerializeField] bool isButtonAnimated;
    public static UnityEvent ContButtonPushed = new UnityEvent();
    public static UnityEvent LoadsButtonPushed = new UnityEvent();
    public static UnityEvent NGButtonPushed = new UnityEvent();
    public static UnityEvent SettingsButtonPushed = new UnityEvent();
    public static UnityEvent AchievmentsButtonPushed = new UnityEvent();
    public static UnityEvent CreditButtonPushed = new UnityEvent();
    public static UnityEvent ExitButtonPushed = new UnityEvent();

    public static UnityEvent MapButtonPushed = new UnityEvent();
    public static UnityEvent NotesButtonPushed = new UnityEvent();
    public static UnityEvent CollectablesButtonPushed = new UnityEvent();
    public static UnityEvent ExitMainTerminalButtonPushed = new UnityEvent();

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!button.IsInteractable())
            return;

        if (buttonsNumber == ButtonsType.cont)
            ContButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.loads)
            LoadsButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.start)
            NGButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.sett)
            SettingsButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.achievments)
            AchievmentsButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.credits)
            CreditButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.exit)
            ExitButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.map)
            MapButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.map)
            NotesButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.collectables)
            CollectablesButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.exitMainTerminal)
            ExitMainTerminalButtonPushed.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!button.IsInteractable())
            return;

        if (isButtonAnimated)
        {
            PlayHoverEffect();
            gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnDisable();
    }

    private void OnDisable()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    private void PlayHoverEffect()
    {
        AudioManager.Instance.PlaySFX("Menu_click");
    }
}

public enum ButtonsType
{
    cont, loads, start, sett, credits, achievments, exit, map, notes, collectables, exitMainTerminal
}
