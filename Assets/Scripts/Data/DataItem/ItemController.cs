using UnityEngine;

/// <summary>
/// Управляет состоянием предмета
/// </summary>
public class ItemController : MonoBehaviour
{
    public Item item;

    private void Start()
    {
        DataItem.GetItem(item.itemName);
        Debug.Log($"{item.itemName}, {item.isPickedUp}");
        if (item.isPickedUp)
        {
            Destroy(gameObject);
        }
    }
}
