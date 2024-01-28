using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "OutfitDataContainer", menuName = "ScriptableObjects/OutfitDataContainer", order = 1)]
public class OutfitDataContainer : ScriptableObject
{
    public List<OutfitData> OutfitDataList;
    public List<OutfitDataAnimation> OutfitDataAnimationList;
}
