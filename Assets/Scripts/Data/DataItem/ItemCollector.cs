using System.Diagnostics;
using UnityEngine;

/// <summary>
/// Отвечает за сбор предметов
/// </summary>
public class ItemCollector : MonoBehaviour
{
    private CharacterHealth characterHealth;

    private void Start()
    {
        characterHealth = GetComponent<CharacterHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            ItemController itemController = collision.gameObject.GetComponent<ItemController>();
            if (itemController != null)
            {
                ItemHandler(itemController, collision); 
            }
        }
    }

    private void ItemHandler(ItemController itemController, Collider2D collision)
    {
        switch (itemController.item.itemType)
        {
            case ItemType.HammerForm:

                Data.IsHammerFormAvailable = true;
                gameObject.GetComponent<Character>().UpdateStates();
                RegisterItem(itemController, collision);
                break;

            case ItemType.Kit:

                if (characterHealth.CurrentHealth == characterHealth.MaxHealth)
                {
                    return;
                }
                else
                {
                    characterHealth.ResetHealth();
                    RegisterItem(itemController, collision);
                }
                break;

            case ItemType.Plot:

                RegisterItem(itemController, collision);

                break;
        }
    }

    private void RegisterItem(ItemController itemController, Collider2D collision)
    {
        itemController.item.isPickedUp = true;
        DataItem.AddItem(itemController.item);
        Destroy(collision.gameObject);
    }
}