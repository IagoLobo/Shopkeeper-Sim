using System.Collections;
using System.Collections.Generic;
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
            Debug.Log("NOT ENOUGH MONEY!");
            return;
        }

        // If buying item, remove money from player
        // If selling item, add half of the original price of that item
        int itemPrice = buyingItem ? -Item.ItemPrice : Mathf.CeilToInt(Item.ItemPrice / 2f);

        InventoryManager.Instance.AddMoney(itemPrice);
        ShopMenuController.Instance.UpdatePlayerMoneyText();

        if(buyingItem)
        {
            ShopMenuController.Instance.RemoveItemFromShopkeeperList(Item);
            ShopMenuController.Instance.UpdateShopkeeperStockItems();
        }
        else
        {
            InventoryManager.Instance.RemoveItem(Item);
            ShopMenuController.Instance.UpdatePlayerStockItems();
        }
    }
}
