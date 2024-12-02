using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MMButtonsBeh : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] ButtonsType buttonsNumber;
    public static UnityEvent ContButtonPushed = new UnityEvent();
    public static UnityEvent NGButtonPushed = new UnityEvent();

    public void OnPointerClick(PointerEventData eventData)
    {
        if (buttonsNumber == ButtonsType.cont)
            ContButtonPushed.Invoke();
        if (buttonsNumber == ButtonsType.start)
            NGButtonPushed.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayHoverEffect();
        gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayHoverEffect();
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    private void PlayHoverEffect()
    {
        ;
    }
}

public enum ButtonsType
{
    cont, loads, start, sett, credits, achievments, exit
}
