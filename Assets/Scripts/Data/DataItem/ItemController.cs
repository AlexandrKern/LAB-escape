using UnityEngine;

/// <summary>
/// Управляет состоянием предмета
/// </summary>
public class ItemController : MonoBehaviour
{
    public Item item;

    private void Awake()
    {
        Item currentItem = DataItem.GetItem(item.itemName);
        if (currentItem != null)
        {
            item = currentItem;
            if (item.isPickedUp)
            {
                Destroy(gameObject);
            }
        }
    }
}
