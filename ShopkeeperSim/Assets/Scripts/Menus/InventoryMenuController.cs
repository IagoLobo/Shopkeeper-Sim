using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryMenuController : MonoBehaviour
{
    public static InventoryMenuController Instance;

    public bool IsInventoryMenuOpen { get; private set; }

    [Header("--- Canvas ---")]
    [SerializeField] private GameObject m_inventoryMenuCanvas;

    [Header("--- Player Inventory Grid ---")]
    [SerializeField] private GameObject m_playerInventoryGrid;
    [SerializeField] private GameObject m_playerItemSlotPrefab;
    private List<GameObject> m_currentPlayerInventory;
    [SerializeField] private GameObject m_noItemText;
    private List<InventoryItemSlot> m_currentPlayerInventorySlots;

    [Header("--- Player Images ---")]
    [SerializeField] private Image m_playerHeadImage;
    [SerializeField] private Image m_playerOutfitImage;
    [SerializeField] private PlayerOutfit m_playerOutfitReference;

    [Header("Your Money")]
    [SerializeField] private TextMeshProUGUI m_yourMoneyText;

    [Header("Back Button")]
    [SerializeField] private GameObject m_backButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);

        IsInventoryMenuOpen = false;
    }

    public void ActivateInventoryMenu(bool activate)
    {
        m_inventoryMenuCanvas.SetActive(activate);
        IsInventoryMenuOpen = activate;
        UpdatePlayerMoneyText();
        EventManager.RaiseOnInventoryMenuActivation();
        UpdatePlayerInventoryMenuItems();
        UpdatePlayerImages();
        UpdateInventorySlotListEquipText();

        EventSystem.current.SetSelectedGameObject(m_backButton);
    }

    private void UpdatePlayerMoneyText()
    {
        m_yourMoneyText.text = InventoryManager.Instance.PlayerMoney + " G";
    }

    public void UpdatePlayerInventoryMenuItems()
    {
        // Remove all previous item slots, if there are any
        if (m_currentPlayerInventory != null && m_currentPlayerInventory.Count > 0)
        {
            foreach (GameObject obj in m_currentPlayerInventory)
            {
                Destroy(obj);
            }

            m_currentPlayerInventory.Clear();
            m_currentPlayerInventorySlots.Clear();
        }
        else
        {
            m_currentPlayerInventory = new List<GameObject>();
            m_currentPlayerInventorySlots = new List<InventoryItemSlot>();
        }

        foreach (ItemData playerItem in InventoryManager.Instance.PlayerInventory)
        {
            // Create new item slot in the UI and update its information
            GameObject itemObj = Instantiate(m_playerItemSlotPrefab, m_playerInventoryGrid.transform);
            m_currentPlayerInventory.Add(itemObj);

            InventoryItemSlot itemSlot = itemObj.GetComponent<InventoryItemSlot>();
            m_currentPlayerInventorySlots.Add(itemSlot);

            itemSlot.Item = playerItem;
            itemSlot.ItemImage.sprite = playerItem.ItemIcon;
        }

        // If there's no item left, show out of stock text to player
        bool showNoItemText = m_currentPlayerInventory.Count <= 0;
        m_noItemText.SetActive(showNoItemText);
    }

    public void UpdatePlayerImages()
    {
        m_playerHeadImage.sprite = m_playerOutfitReference.CurrentPlayerOutfit.CharacterHeadItem.OutfitSprite;
        m_playerOutfitImage.sprite = m_playerOutfitReference.CurrentPlayerOutfit.CharacterOutfitItem.OutfitSprite;
    }

    public void UpdateInventorySlotListEquipText()
    {
        if(m_currentPlayerInventorySlots.Count > 0)
        {
            foreach (InventoryItemSlot itemSlot in m_currentPlayerInventorySlots)
            {
                // If it's different IDs, this item is not equipped, write Equip
                if (itemSlot.Item.ItemID != m_playerOutfitReference.CurrentPlayerOutfit.CharacterHeadItem.OutfitID || itemSlot.Item.ItemID != m_playerOutfitReference.CurrentPlayerOutfit.CharacterOutfitItem.OutfitID)
                {
                    itemSlot.ToggleEquipText(true);
                }

                // If it's the same ID, this item is already equipped, write Unequip
                if(itemSlot.Item.ItemID == m_playerOutfitReference.CurrentPlayerOutfit.CharacterHeadItem.OutfitID || itemSlot.Item.ItemID == m_playerOutfitReference.CurrentPlayerOutfit.CharacterOutfitItem.OutfitID)
                {
                    itemSlot.ToggleEquipText(false);
                }
            }
        }
    }

    public PlayerOutfit GetPlayerOutfit()
    {
        return m_playerOutfitReference;
    }
}