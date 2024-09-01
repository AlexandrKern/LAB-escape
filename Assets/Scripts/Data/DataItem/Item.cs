using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Item", fileName = "New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;
    [HideInInspector] public bool isPickedUp;
}
