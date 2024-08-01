using UnityEngine;

namespace CodeBase.StaticData.ColorsRegistry
{
    [CreateAssetMenu(fileName = "ColorConfig", menuName = "StaticData/Colors/ColorConfig", order = 1)]
    public class ColorConfig : ScriptableObject
    {
        public RarityType RarityType;
        [ColorUsage(true, true)]
        public Color32 Color;
    }
}