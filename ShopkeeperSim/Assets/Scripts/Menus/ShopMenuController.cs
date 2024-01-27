using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenuController : MonoBehaviour
{
    public static ShopMenuController Instance;

    public bool IsShopMenuOpen {get; private set;}

    [SerializeField] private GameObject m_shopMenuCanvas;
    [SerializeField] private GameObject m_welcomeScreen;
    [SerializeField] private GameObject m_buyScreen;
    [SerializeField] private GameObject m_sellScreen;

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
    }

    public void ActivateSellScreen(bool activate)
    {
        m_welcomeScreen.SetActive(!activate);
        m_buyScreen.SetActive(!activate);
        m_sellScreen.SetActive(activate);
    }

    public void ActivateWelcomeScreen(bool activate)
    {
        m_welcomeScreen.SetActive(activate);
        m_buyScreen.SetActive(!activate);
        m_sellScreen.SetActive(!activate);
    }
}
