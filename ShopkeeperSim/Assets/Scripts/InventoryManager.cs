using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemData> PlayerInventory;
    public int PlayerMoney { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);

        PlayerMoney = 0;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Submit") &&  !ShopMenuController.Instance.IsShopMenuOpen)
        {
            // Open inventory menu here
        }
    }

    public void AddItem(ItemData item)
    {
        PlayerInventory.Add(item);
    }

    public void RemoveItem(ItemData item)
    {
        PlayerInventory.Remove(item);
    }

    public void AddMoney(int amountToAdd)
    {
        // Get current amount and sum to value given
        int newAmount = PlayerMoney + amountToAdd;

        // Check if the new amount is valid, min cap is 0 and max cap is 999
        PlayerMoney = newAmount > 0 ? newAmount : 0;
        PlayerMoney = newAmount < 1000 ? newAmount : 999;
    }
}
