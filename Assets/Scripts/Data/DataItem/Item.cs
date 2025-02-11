using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Item", fileName = "New Item")]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public string description;
    [Newtonsoft.Json.JsonIgnore]
    public Sprite icon;
    [HideInInspector] public bool isPickedUp;
}
