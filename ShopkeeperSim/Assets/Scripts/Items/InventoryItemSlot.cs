using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlot : MonoBehaviour
{
    [HideInInspector] public ItemData Item;
    public Image ItemImage;
    [SerializeField] private TextMeshProUGUI m_equipText;

    public void EquipItem()
    {
        PlayerOutfit playerOutfit = InventoryMenuController.Instance.GetPlayerOutfit();

        if (playerOutfit.CurrentPlayerOutfit.CharacterOutfitItem.OutfitID == Item.ItemID || playerOutfit.CurrentPlayerOutfit.CharacterHeadItem.OutfitID == Item.ItemID)
        {
            InventoryManager.Instance.UnequipOutfit(Item.ItemID);
            ToggleEquipText(true);
        }
        else
        {
            InventoryManager.Instance.EquipOutfit(Item.ItemID);
            ToggleEquipText(false);
        }

        InventoryMenuController.Instance.UpdatePlayerImages();
        InventoryMenuController.Instance.UpdateInventorySlotListEquipText();
    }

    public void ToggleEquipText(bool equipItem)
    {
        m_equipText.text = equipItem ? "Equip" : "Unequip";
    }
}
