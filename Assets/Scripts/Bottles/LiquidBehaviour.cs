using System;
using UnityEngine;

namespace Bottles
{
    public class LiquidBehaviour : MonoBehaviour
    {

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

        public void Update()
        {
            _lifetime -= Time.deltaTime;
            if (_lifetime < 0)
                Destroy(this.gameObject);
        }
    }
}