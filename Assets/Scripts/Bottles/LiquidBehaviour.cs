using System;
using Orders;
using UnityEngine;

namespace Bottles
{
    public class LiquidBehaviour : MonoBehaviour
    {
        public Material Material;
        
        public Color Color;

        public LiquidColor LiquidColor;

        /// <summary>
        /// Lifetime, in seconds.
        /// </summary>
        private float _lifetime = 30;

        
        
        
        public void Awake()
        {
            transform.localScale = transform.localScale * (.6f + UnityEngine.Random.value * 0.4f);
            
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.mass = 1 + UnityEngine.Random.value * 9;
            rb.drag = UnityEngine.Random.value * 10;
            rb.angularDrag = UnityEngine.Random.value * 10;
        }

        public void Init(LiquidColor col)
        {
            this.LiquidColor = col;
            this.Color = col.GetColor();
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            renderer.material = new Material(Material);
            renderer.material.SetColor("_Color", this.Color);
        }
        

        public void Update()
        {
            _lifetime -= Time.deltaTime;
            if (_lifetime < 0)
                Destroy(this.gameObject);
        }
    }
}