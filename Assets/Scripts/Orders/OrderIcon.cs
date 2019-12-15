﻿using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Orders
{
    public class OrderIcon : MonoBehaviour
    {
        public Image ImgLifetime;
        
        public Image ImgColorOne;
        public Image ImgColorTwo;

        public float Lifetime = 60;
        
        private bool _isRunning = false;

        private OrdersCanvas _canvas;
        
        // Start is called before the first frame update
        public void Init(OrdersCanvas canvas, Order order)
        {
            _canvas = canvas;
            ImgColorOne.color = new Color(Random.value, Random.value, Random.value);
            ImgColorTwo.color = new Color(Random.value, Random.value, Random.value);
        }

        // Update is called once per frame
        void Update()
        {
            this.Lifetime -= Time.deltaTime;
            if (this.Lifetime < 0)
            {
                Debug.LogError("ORDER EXPIRED!!");
                _canvas.DequeueOrder(false);
            }

            ImgLifetime.fillAmount = Math.Max(0, this.Lifetime / 60f);
        }
    }
}
