using System;
using System.Collections.Generic;
using Bottles;
using Orders;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Glasses
{
    public class GlassInteriorBehaviour : MonoBehaviour
    {
        private static readonly int mat_Tint = Shader.PropertyToID("_Tint");
        private static readonly int mat_FillAmount = Shader.PropertyToID("_FillAmount");
        
        

        public Material LiquidMaterial;

        public float FillAmount;
        private Dictionary<LiquidColor, float> _mixes; // Dictionary telling how much of each liquid we have
        
        

        private MeshRenderer _renderer;
        private Color _color;


        public void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
            _renderer.material = new Material(LiquidMaterial);

            this.FillAmount = 0;
            _mixes = new Dictionary<LiquidColor, float>();
            _renderer.material.SetFloat(mat_FillAmount, this.FillAmount);
        }
        
        
        private void OnCollisionEnter(Collision other)
        {
            if (this.FillAmount >= 1f)
                return;
            
            if (other.gameObject.CompareTag("liquid"))
            {
                Debug.Log("LIQUID COLLISION!!");
                Fill(other.transform.GetComponent<LiquidBehaviour>());
                Destroy(other.gameObject);
            }
        }

        private void Fill(LiquidBehaviour liquid)
        {
            // CONTROL QUANTITIES
            float increase = 0.3f * Time.deltaTime;
            float prev = FillAmount;

            FillAmount += increase;
            float unclamped = FillAmount;
            
            if (FillAmount > 1f)
                FillAmount = 1f;

            
            // ADD TO OUR MIX DICTIONARY
            LiquidColor lCol = liquid.LiquidColor;
            if (!_mixes.ContainsKey(lCol))
                _mixes.Add(lCol, 0);

            _mixes[lCol] += FillAmount - prev;
            
            
            // MIX ACTUAL COLOR
            _color = new Color(
                (_color.r * prev / unclamped) + (liquid.Color.r * increase / unclamped),
                (_color.g * prev / unclamped) + (liquid.Color.g * increase / unclamped),
                (_color.b * prev / unclamped) + (liquid.Color.b * increase / unclamped)
            );
            
            _renderer.material.SetFloat(mat_FillAmount, this.FillAmount);
            _renderer.material.SetColor(mat_Tint, _color);
        }


        /// <summary>
        /// Check if we match a given <see cref="Order"/>.
        /// We'll obtain 0.01 point for each correct percentage of
        /// liquid mix.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public float Check(Order order)
        {
            float score = 0;
            
            if (_mixes.ContainsKey(order.ColorOne))
            {
                float quantityOne = _mixes[order.ColorOne];
                if (quantityOne > 0.5f)
                    quantityOne = 0.5f;

                score += quantityOne;
            }
            if (_mixes.ContainsKey(order.ColorTwo))
            {
                float quantityTwo = _mixes[order.ColorTwo];
                
                if (order.ColorOne == order.ColorTwo)
                    quantityTwo -= 0.5f;
                else if (quantityTwo > 0.5f)
                    quantityTwo = 0.5f;

                if (quantityTwo < 0)
                    quantityTwo = 0;

                score += quantityTwo;
            }

            return score;
        }
    }
}
