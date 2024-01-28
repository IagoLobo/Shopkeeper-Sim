using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [HideInInspector] public ItemData Item;
    public TextMeshProUGUI ItemNameText;
    public Image ItemImage;
    public TextMeshProUGUI ItemPriceText;

    public void TradeItem(bool buyingItem)
    {
        // Player needs enough money to buy item
        if(InventoryManager.Instance.PlayerMoney < Item.ItemPrice && buyingItem)
        {
            // Not enough money, don't trade item
            ShopMenuController.Instance.FlashMoneyText();
            return;
        }

        // If buying item, remove money from player
        // If selling item, add half of the original price of that item
        int itemPrice = buyingItem ? -Item.ItemPrice : Mathf.CeilToInt(Item.ItemPrice / 2f);

        InventoryManager.Instance.AddMoney(itemPrice);
        ShopMenuController.Instance.UpdatePlayerMoneyText();

        if(buyingItem)
        {
            // Add item to player inventory and update current shopkeeper stock
            InventoryManager.Instance.AddItem(Item);
            ShopMenuController.Instance.RemoveItemFromShopkeeperList(Item);
            ShopMenuController.Instance.UpdateShopkeeperStockItems();
        }
        else
        {
            // Remove item from player's inventory and update player stock in the UI
            InventoryManager.Instance.RemoveItem(Item);
            ShopMenuController.Instance.UpdatePlayerStockItems();
        }
    }
}
