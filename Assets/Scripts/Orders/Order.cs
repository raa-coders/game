using UnityEngine;

namespace Orders
{
    public struct Order
    {
        public static readonly int COLOR_COUNT = 3;
        
        
        public LiquidColor ColorOne;
        public LiquidColor ColorTwo;


        public static Order Create()
        {
            int colOne = Mathf.FloorToInt(Random.value * COLOR_COUNT);
            int colTwo = Mathf.FloorToInt(Random.value * COLOR_COUNT);

            return new Order()
            {
                ColorOne = (LiquidColor) colOne,
                ColorTwo = (LiquidColor) colTwo
            };
        }
    }
    
}