using UnityEngine;

/// <summary>
/// �������� �� ���� ���������
/// </summary>
public class ItemCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            var itemController = collision.gameObject.GetComponent<ItemController>();
            if (itemController != null)
            {
                itemController.item.isPickedUp = true;
                
                if(itemController.item.itemName == "Hammer skill")
                {
                    Data.IsHammerFormAvailable = true;
                    gameObject.GetComponent<Character>().UpdateStates();
                }

                DataItem.AddItem(itemController.item);
                Destroy(collision.gameObject);
            }
        }
    }
}