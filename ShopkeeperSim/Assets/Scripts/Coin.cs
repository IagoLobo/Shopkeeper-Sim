using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int m_coinValue;
    private bool m_isPlayerClose;

    private void Awake()
    {
        m_isPlayerClose = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit") && m_isPlayerClose)
        {
            // Give the money to the player
            InventoryManager.Instance.AddMoney(m_coinValue);

            // Deactivate object from scene
            m_isPlayerClose = false;
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
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
