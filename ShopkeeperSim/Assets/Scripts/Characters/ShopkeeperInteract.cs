using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperInteract : MonoBehaviour
{
    [SerializeField] private ShopMenuController m_shopMenuController;
    private bool m_isPlayerClose;

    private void Awake()
    {
        m_isPlayerClose = false;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Submit") && m_isPlayerClose)
        {
            m_shopMenuController.ActivateShopMenu(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            m_isPlayerClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_isPlayerClose = false;
        }
    }
}
