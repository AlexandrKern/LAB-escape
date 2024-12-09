using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MMButtonsBeh : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Button button;
    [SerializeField] ButtonsType buttonsNumber;
    public static UnityEvent ContButtonPushed = new UnityEvent();
    public static UnityEvent NGButtonPushed = new UnityEvent();
    public static UnityEvent SettingsButtonPushed = new UnityEvent();

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!button.IsInteractable())
            return;

        if (buttonsNumber == ButtonsType.cont)
            ContButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.start)
            NGButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.sett)
            SettingsButtonPushed.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!button.IsInteractable())
            return;

        PlayHoverEffect();
        gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1);
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
    cont, loads, start, sett, credits, achievments, exit
}
