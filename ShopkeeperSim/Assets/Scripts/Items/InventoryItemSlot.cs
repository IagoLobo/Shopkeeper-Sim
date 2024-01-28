using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlot : MonoBehaviour
{
    [HideInInspector] public ItemData Item;
    public Image ItemImage;
    [SerializeField] private GameObject m_equipText;

    public void EquipItem()
    {
        
    }
}
