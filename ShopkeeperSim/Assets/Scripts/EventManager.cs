using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnShopMenuActivation();
    public static event OnShopMenuActivation onShopMenuActivation;
    
    public static void RaiseOnShopMenuActivation()
    {
        if (onShopMenuActivation != null)
        {
            onShopMenuActivation();
        }
    }
}
