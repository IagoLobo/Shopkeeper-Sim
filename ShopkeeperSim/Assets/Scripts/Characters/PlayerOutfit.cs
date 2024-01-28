using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Outfit
{
    public OutfitData CharacterHeadItem;
    public OutfitData CharacterOutfitItem;
}

public class PlayerOutfit : MonoBehaviour
{
    public Outfit CurrentPlayerOutfit;

    [SerializeField] private SpriteRenderer m_charHead;
    [SerializeField] private SpriteRenderer m_charOutfit;

    public void SetHead(Sprite headSprite)
    {
        m_charHead.sprite = headSprite;
    }

    public void SetOutfit(Sprite outfitSprite)
    {
        m_charOutfit.sprite = outfitSprite;
    }
}
