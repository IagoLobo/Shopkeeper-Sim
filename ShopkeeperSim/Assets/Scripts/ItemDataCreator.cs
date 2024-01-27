using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemDataCreator : ScriptableObject
{
    public int itemID;
    public string itemName;
    public int itemPrice;
    public Sprite itemIcon;
}
