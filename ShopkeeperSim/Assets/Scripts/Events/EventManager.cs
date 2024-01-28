using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnShopMenuActivation();
    public static event OnShopMenuActivation onShopMenuActivation;

    public delegate void OnInventoryMenuActivation();
    public static event OnInventoryMenuActivation onInventoryMenuActivation;

    public static void RaiseOnShopMenuActivation()
    {
        if (onShopMenuActivation != null)
        {
            onShopMenuActivation();
        }
    }

    public static void RaiseOnInventoryMenuActivation()
    {
        if (onInventoryMenuActivation != null)
        {
            onInventoryMenuActivation();
        }
    }
}
