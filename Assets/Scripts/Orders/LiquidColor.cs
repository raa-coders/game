using UnityEngine;

namespace Orders
{
    public enum LiquidColor
    {
        Blue,
        Yellow,
        Pink
    }
    
    
    public static class LiquidColorExtensions
    {
        public static Color GetColor(this LiquidColor col)
        {
            switch (col)
            {
                case LiquidColor.Blue:
                    return new Color(0f, 0.69f, 1f);
                
                case LiquidColor.Yellow:
                    return new Color(1f, 0.78f, 0.25f);
                
                case LiquidColor.Pink:
                    return new Color(1f, 0.44f, 0.93f);
            }
            
            return Color.white;
        }
    }
}