using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerClickHandler
{
    public static UnityEvent StartButtonPushed = new UnityEvent();

    public void OnPointerClick(PointerEventData eventData)
    {
        Data.LoadData();
        StartButtonPushed.Invoke();
    }
}
