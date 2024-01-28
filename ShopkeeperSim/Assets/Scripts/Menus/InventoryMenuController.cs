using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryMenuController : MonoBehaviour
{
    public static InventoryMenuController Instance;

    public bool IsInventoryMenuOpen { get; private set; }

    [Header("--- Canvas ---")]
    [SerializeField] private GameObject m_inventoryMenuCanvas;

    [Header("--- Player Inventory ---")]
    [SerializeField] private GameObject m_playerInventoryGrid;
    [SerializeField] private GameObject m_playerItemSlotPrefab;
    private List<GameObject> m_currentPlayerInventory;
    [SerializeField] private GameObject m_noItemText;

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
        }
        else
        {
            m_currentPlayerInventory = new List<GameObject>();
        }

        foreach (ItemData playerItem in InventoryManager.Instance.PlayerInventory)
        {
            // Create new item slot in the UI and update its information
            GameObject itemObj = Instantiate(m_playerItemSlotPrefab, m_playerInventoryGrid.transform);
            m_currentPlayerInventory.Add(itemObj);

            InventoryItemSlot itemSlot = itemObj.GetComponent<InventoryItemSlot>();

            itemSlot.Item = playerItem;
            itemSlot.ItemImage.sprite = playerItem.ItemIcon;
        }

        // If there's no item left, show out of stock text to player
        bool showNoItemText = m_currentPlayerInventory.Count <= 0;
        m_noItemText.SetActive(showNoItemText);
    }
}