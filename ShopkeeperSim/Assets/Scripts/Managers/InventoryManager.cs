using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemData> PlayerInventory;
    public int PlayerMoney { get; private set; }

    public OutfitDataContainer OutfitDataContainerList;
    [SerializeField] private PlayerOutfit m_playerOutfit;
    [SerializeField] private OutfitData m_nothingOutfit;
    [SerializeField] private OutfitData m_nothingHead;

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
        RemoveOutfitFromPlayer(item.ItemID);
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
        OutfitData data = OutfitDataContainerList.OutfitDataList.First(x => x.OutfitID == itemID);

        if (data.IsHeadPiece)
        {
            m_playerOutfit.SetHead(data);
        }
        else
        {
            m_playerOutfit.SetOutfit(data);
        }
    }

    public void UnequipOutfit(int itemID)
    {
        OutfitData data = OutfitDataContainerList.OutfitDataList.First(x => x.OutfitID == itemID);

        if (data.IsHeadPiece)
        {
            m_playerOutfit.SetHead(m_nothingHead);
        }
        else
        {
            m_playerOutfit.SetOutfit(m_nothingOutfit);
        }
    }

    private void RemoveOutfitFromPlayer(int itemID)
    {
        if(m_playerOutfit.CurrentPlayerOutfit.CharacterHeadItem != null)
        {
            if(m_playerOutfit.CurrentPlayerOutfit.CharacterHeadItem.OutfitID == itemID)
            {
                UnequipOutfit(itemID);
            }
        }

        if (m_playerOutfit.CurrentPlayerOutfit.CharacterOutfitItem != null)
        {
            if (m_playerOutfit.CurrentPlayerOutfit.CharacterOutfitItem.OutfitID == itemID)
            {
                UnequipOutfit(itemID);
            }
        }
    }
}
