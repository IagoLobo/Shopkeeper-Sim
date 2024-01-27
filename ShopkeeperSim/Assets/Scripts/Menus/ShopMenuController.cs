using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopMenuController : MonoBehaviour
{
    public static ShopMenuController Instance;

    public bool IsShopMenuOpen {get; private set;}

    [Header("Canvas")]
    [SerializeField] private GameObject m_shopMenuCanvas;

    [Header("Welcome Screen")]
    [SerializeField] private GameObject m_welcomeScreen;

    [Header("Buy Screen")]
    [SerializeField] private GameObject m_buyScreen;
    [SerializeField] private TextMeshProUGUI m_yourMoneyBuyScreenText;

    [Header("Sell Screen")]
    [SerializeField] private GameObject m_sellScreen;
    [SerializeField] private TextMeshProUGUI m_yourMoneySellScreenText;

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
            UpdateMoneyText(m_yourMoneyBuyScreenText);
        }
    }

    public void ActivateSellScreen(bool activate)
    {
        m_welcomeScreen.SetActive(!activate);
        m_buyScreen.SetActive(!activate);
        m_sellScreen.SetActive(activate);

        if(activate)
        {
            UpdateMoneyText(m_yourMoneySellScreenText);
        }
    }

    public void ActivateWelcomeScreen(bool activate)
    {
        m_welcomeScreen.SetActive(activate);
        m_buyScreen.SetActive(!activate);
        m_sellScreen.SetActive(!activate);
    }

    private void UpdateMoneyText(TextMeshProUGUI textRef)
    {
        textRef.text = InventoryManager.Instance.PlayerMoney + " G";
    }
}
