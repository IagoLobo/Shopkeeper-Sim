using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public struct Outfit
{
    public OutfitData CharacterHeadItem;
    public OutfitData CharacterOutfitItem;
}

public class PlayerOutfit : MonoBehaviour
{
    public Outfit CurrentPlayerOutfit;

    [SerializeField] private Animator m_charHead;
    [SerializeField] private Animator m_charOutfit;

    public void SetHead(OutfitData data)
    {
        CurrentPlayerOutfit.CharacterHeadItem = data;
        m_charHead.runtimeAnimatorController = InventoryManager.Instance.OutfitDataContainerList.OutfitDataAnimationList.First(x => x.Outfit.OutfitID == data.OutfitID).OutfitAnimator;
    }

    public void SetOutfit(OutfitData data)
    {
        CurrentPlayerOutfit.CharacterOutfitItem = data;
        m_charOutfit.runtimeAnimatorController = InventoryManager.Instance.OutfitDataContainerList.OutfitDataAnimationList.First(x => x.Outfit.OutfitID == data.OutfitID).OutfitAnimator;
    }
}
