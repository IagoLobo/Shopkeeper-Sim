using UnityEngine;

[CreateAssetMenu(fileName = "OutfitDataAnimation", menuName = "ScriptableObjects/OutfitDataAnimation", order = 1)]
public class OutfitDataAnimation : ScriptableObject
{
    public OutfitData Outfit;
    public AnimatorOverrideController OutfitAnimator;
}
