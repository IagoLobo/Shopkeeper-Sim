using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [SerializeField] private List<ItemData> m_playerInventory;
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
        m_playerInventory.Add(item);
    }

    public void RemoveItem(ItemData item)
    {
        m_playerInventory.Remove(item);
    }

    public void AddMoney(int amountToAdd)
    {
        int newAmount = PlayerMoney + amountToAdd;
        PlayerMoney = newAmount > 0 ? newAmount : 0;
    }
}
