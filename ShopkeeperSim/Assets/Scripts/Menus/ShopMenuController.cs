using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopMenuController : MonoBehaviour
{
    public static ShopMenuController Instance;

    public bool IsShopMenuOpen {get; private set;}

    [Header("--- Canvas ---")]
    [SerializeField] private GameObject m_shopMenuCanvas;

    [Header("Welcome Screen")]
    [SerializeField] private GameObject m_welcomeScreen;
    [SerializeField] private GameObject m_buyButton;

    [Header("Buy Screen")]
    [SerializeField] private GameObject m_buyScreen;
    [SerializeField] private TextMeshProUGUI m_yourMoneyBuyScreenText;
    [SerializeField] private GameObject m_outOfStockBuyScreen;

    [Header("Sell Screen")]
    [SerializeField] private GameObject m_sellScreen;
    [SerializeField] private TextMeshProUGUI m_yourMoneySellScreenText;
    [SerializeField] private GameObject m_outOfStockSellScreen;

    [Header("--- Shopkeeper Stock ---")]
    [SerializeField] private List<ItemData> m_shopkeeperStock;
    [SerializeField] private GameObject m_shopStockList;
    [SerializeField] private GameObject m_buyItemSlot;
    private List<GameObject> m_currentShopStock;

    [Header("--- Player Stock ---")]
    [SerializeField] private GameObject m_playerStockList;
    [SerializeField] private GameObject m_sellItemSlot;
    private List<GameObject> m_currentPlayerStock;

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

        if(activate)
        {
            EventSystem.current.SetSelectedGameObject(m_buyButton);
        }
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

        UpdatePlayerStockItems();
    }

    public void ActivateWelcomeScreen(bool activate)
    {
        m_welcomeScreen.SetActive(activate);
        m_buyScreen.SetActive(!activate);
        m_sellScreen.SetActive(!activate);

        EventSystem.current.SetSelectedGameObject(m_buyButton);
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
            GameObject itemObj = Instantiate(m_buyItemSlot, m_shopStockList.transform);
            m_currentShopStock.Add(itemObj);

            ItemSlot itemSlot = itemObj.GetComponent<ItemSlot>();

            itemSlot.Item = shopItem;
            itemSlot.ItemNameText.text = shopItem.ItemName;
            itemSlot.ItemImage.sprite = shopItem.ItemIcon;
            itemSlot.ItemPriceText.text = shopItem.ItemPrice + " G";
        }

        // If there's no item left, show out of stock text to player
        if(m_currentShopStock.Count <= 0)
        {
            m_outOfStockBuyScreen.SetActive(true);
        }
        else
        {
            // Select the first item in the options
            EventSystem.current.SetSelectedGameObject(m_currentShopStock[0]);
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

    public void UpdatePlayerStockItems()
    {
        // Remove all previous item slots, if there are any
        if (m_currentPlayerStock != null && m_currentPlayerStock.Count > 0)
        {
            foreach (GameObject obj in m_currentPlayerStock)
            {
                Destroy(obj);
            }

            m_currentPlayerStock.Clear();
        }
        else
        {
            m_currentPlayerStock = new List<GameObject>();
        }

        foreach (ItemData playerItem in InventoryManager.Instance.PlayerInventory)
        {
            // Create new item slot in the UI and update its information
            GameObject itemObj = Instantiate(m_sellItemSlot, m_playerStockList.transform);
            m_currentPlayerStock.Add(itemObj);

            ItemSlot itemSlot = itemObj.GetComponent<ItemSlot>();

            itemSlot.Item = playerItem;
            itemSlot.ItemNameText.text = playerItem.ItemName;
            itemSlot.ItemImage.sprite = playerItem.ItemIcon;
            itemSlot.ItemPriceText.text = Mathf.CeilToInt(itemSlot.Item.ItemPrice / 2f) + " G";
        }

        // If there's no item left, show out of stock text to player
        if (m_currentPlayerStock.Count <= 0)
        {
            m_outOfStockSellScreen.SetActive(true);
        }
        else
        {
            // Select the first item in the options
            EventSystem.current.SetSelectedGameObject(m_currentPlayerStock[0]);
        }
    }
}
