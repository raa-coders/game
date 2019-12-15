using System;
using Bottles;
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

        private MeshRenderer _renderer;
        private Color _color;


        public void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
            _renderer.material = new Material(LiquidMaterial);

            this.FillAmount = 0;
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
            float increase = 0.3f * Time.deltaTime;
            float prev = FillAmount;

            FillAmount += increase;
            float unclamped = FillAmount;
            
            if (FillAmount > 1f)
                FillAmount = 1f;
            
            _color = new Color(
                (_color.r * prev / unclamped) + (liquid.Color.r * increase / unclamped),
                (_color.g * prev / unclamped) + (liquid.Color.g * increase / unclamped),
                (_color.b * prev / unclamped) + (liquid.Color.b * increase / unclamped)
            );
            
            _renderer.material.SetFloat(mat_FillAmount, this.FillAmount);
            _renderer.material.SetColor(mat_Tint, _color);
        }
    }
}
