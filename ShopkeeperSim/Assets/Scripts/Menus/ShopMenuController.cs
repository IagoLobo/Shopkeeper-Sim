using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopMenuController : MonoBehaviour
{
    public static ShopMenuController Instance;

    public bool IsShopMenuOpen {get; private set;}

    [Header("--- Canvas ---")]
    [SerializeField] private GameObject m_shopMenuCanvas;

    [Header("Welcome Screen")]
    [SerializeField] private GameObject m_welcomeScreen;

    [Header("Buy Screen")]
    [SerializeField] private GameObject m_buyScreen;
    [SerializeField] private TextMeshProUGUI m_yourMoneyBuyScreenText;

    [Header("Sell Screen")]
    [SerializeField] private GameObject m_sellScreen;
    [SerializeField] private TextMeshProUGUI m_yourMoneySellScreenText;

    [Header("--- Shopkeeper Stock ---")]
    [SerializeField] private List<ItemData> m_shopkeeperStock;
    [SerializeField] private GameObject m_shopStockList;
    [SerializeField] private GameObject m_itemSlot;
    private List<GameObject> m_currentShopStock;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);

        IsShopMenuOpen = false;
    }

    public void ActivateShopMenu(bool activate)
    {
        m_shopMenuCanvas.SetActive(activate);
        IsShopMenuOpen = activate;
        EventManager.RaiseOnShopMenuActivation();
    }

    public void ActivateBuyScreen(bool activate)
    {
        m_welcomeScreen.SetActive(!activate);
        m_buyScreen.SetActive(activate);
        m_sellScreen.SetActive(!activate);

        if (activate)
        {
            UpdatePlayerMoneyText();
        }

        UpdateShopkeeperStockItems();
    }

    public void ActivateSellScreen(bool activate)
    {
        m_welcomeScreen.SetActive(!activate);
        m_buyScreen.SetActive(!activate);
        m_sellScreen.SetActive(activate);

        if(activate)
        {
            UpdatePlayerMoneyText();
        }
    }

    public void ActivateWelcomeScreen(bool activate)
    {
        m_welcomeScreen.SetActive(activate);
        m_buyScreen.SetActive(!activate);
        m_sellScreen.SetActive(!activate);
    }

    public void RemoveItemFromShopkeeperList(ItemData item)
    {
        m_shopkeeperStock.Remove(item);
    }

    public void UpdateShopkeeperStockItems()
    {
        // Remove all previous item slots, if there are any
        if(m_currentShopStock != null && m_currentShopStock.Count > 0)
        {
            foreach(GameObject obj in m_currentShopStock)
            {
                Destroy(obj);
            }

            m_currentShopStock.Clear();
        }
        else
        {
            m_currentShopStock = new List<GameObject>();
        }

        foreach(ItemData shopItem in m_shopkeeperStock)
        {
            // Create new item slot in the UI and update its information
            GameObject itemObj = Instantiate(m_itemSlot, m_shopStockList.transform);
            m_currentShopStock.Add(itemObj);

            ItemSlot itemSlot = itemObj.GetComponent<ItemSlot>();

            itemSlot.Item = shopItem;
            itemSlot.ItemNameText.text = shopItem.ItemName;
            itemSlot.ItemImage.sprite = shopItem.ItemIcon;
            itemSlot.ItemPriceText.text = shopItem.ItemPrice + " G";
        }
    }

    public void UpdatePlayerMoneyText()
    {
        UpdatePlayerMoneyText(m_yourMoneyBuyScreenText);
        UpdatePlayerMoneyText(m_yourMoneySellScreenText);
    }

    private void UpdatePlayerMoneyText(TextMeshProUGUI textRef)
    {
        textRef.text = InventoryManager.Instance.PlayerMoney + " G";
    }
}
