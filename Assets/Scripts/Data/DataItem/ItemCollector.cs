using UnityEngine;

/// <summary>
/// Отвечает за сбор предметов
/// </summary>
public class ItemCollector : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            var itemController = collision.gameObject.GetComponent<ItemController>();
            if (itemController != null)
            {
                itemController.item.isPickedUp = true;
                DataItem.AddItem(itemController.item);
                Destroy(collision.gameObject);
            }
        }
    }
}