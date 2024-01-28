using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public int ItemID;
    public string ItemName;
    public int ItemPrice;
    public Sprite ItemIcon;
}
