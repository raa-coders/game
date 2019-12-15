using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

namespace Orders
{
    public class OrdersCanvas : MonoBehaviour
    {
        public Transform SpawnPoint;
        public OrderIcon OrderIconPrefab;

        public TMP_Text TextScore;

        private Queue<OrderIcon> _orders;
        private int _score;

        [SerializeField]
        private float _orderEnqueueCount;
        
        
        public void Awake()
        {
            _orderEnqueueCount = 10;
            _orders = new Queue<OrderIcon>();
            this.TextScore.SetText("0 €");
        }


        public void Update()
        {
            _orderEnqueueCount -= Time.deltaTime;
            if (_orderEnqueueCount <= 0)
            {
                EnqueueOrder();
                _orderEnqueueCount = 25;
            }
        }

        public bool Peek(ref Order order)
        {
            if (_orders.Count <= 0)
                return false;
            
            order = _orders.Peek().Order;
            return true;
        }

        public void EnqueueOrder()
        {
            Order o = Order.Create();
            
            OrderIcon orderIcon = Instantiate(OrderIconPrefab, this.SpawnPoint);
            orderIcon.transform.localPosition = new Vector3(64 * _orders.Count, 0, 0);
            
            orderIcon.Init(this, o);
            _orders.Enqueue(orderIcon);
        }

        public void DequeueOrder(bool success)
        {
            Destroy(_orders.Dequeue());

            foreach (OrderIcon o in _orders)
            {
                o.transform.localPosition -= new Vector3(64, 0, 0);
            }

            if (success)
            {
                // TODO success sound
                _score += 5 + Mathf.CeilToInt(UnityEngine.Random.value * 3);
            }
            else
            {
                // TODO failure sound
                _score -= 2;
            }
            this.TextScore.SetText($"{_score} €");
        }
    }
}