using UnityEngine;

[CreateAssetMenu(fileName = "OutfitData", menuName = "ScriptableObjects/OutfitData", order = 1)]
public class OutfitData : ScriptableObject
{
    public int OutfitID;
    public bool IsHeadPiece;
    public Sprite OutfitSprite;
}
