using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MMButtonsBeh : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Button button;
    [SerializeField] ButtonsType buttonsNumber;
    [SerializeField] bool isButtonAnimated;

    public static UnityEvent OnContButtonPushed = new UnityEvent();
    public static UnityEvent OnLoadsButtonPushed = new UnityEvent();
    public static UnityEvent OnNGButtonPushed = new UnityEvent();
    public static UnityEvent OnSettingsButtonPushed = new UnityEvent();
    public static UnityEvent OnAchievmentsButtonPushed = new UnityEvent();
    public static UnityEvent OnCreditButtonPushed = new UnityEvent();
    public static UnityEvent OnExitButtonPushed = new UnityEvent();

    public static UnityEvent OnMapButtonPushed = new UnityEvent();
    public static UnityEvent OnNotesButtonPushed = new UnityEvent();
    public static UnityEvent OnCollectablesButtonPushed = new UnityEvent();
    public static UnityEvent OnExitMainTerminalButtonPushed = new UnityEvent();

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!button.IsInteractable())
            return;

        if (buttonsNumber == ButtonsType.cont)
            OnContButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.loads)
            OnLoadsButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.start)
            OnNGButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.sett)
            OnSettingsButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.achievments)
            OnAchievmentsButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.credits)
            OnCreditButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.exit)
            OnExitButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.map)
            OnMapButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.map)
            OnNotesButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.collectables)
            OnCollectablesButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.exitMainTerminal)
        {
            OnExitMainTerminalButtonPushed.Invoke();
            Debug.Log("ExitMainTerminal");
        }    
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
