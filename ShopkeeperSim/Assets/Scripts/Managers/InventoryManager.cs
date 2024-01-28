using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemData> PlayerInventory;
    public int PlayerMoney { get; private set; }

    public OutfitDataContainer OutfitDataList;
    [SerializeField] private PlayerOutfit m_playerOutfit;

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
        if (Input.GetButtonDown("Cancel") && !ShopMenuController.Instance.IsShopMenuOpen && !InventoryMenuController.Instance.IsInventoryMenuOpen)
        {
            InventoryMenuController.Instance.ActivateInventoryMenu(true);
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

    public void EquipOutfit(int itemID)
    {
        OutfitData data = OutfitDataList.outfitDataList.First(x => x.OutfitID == itemID);

        if (data.IsHeadPiece)
        {
            m_playerOutfit.SetHead(data.OutfitSprite);
        }
        else
        {
            m_playerOutfit.SetOutfit(data.OutfitSprite);
        }
    }
}
