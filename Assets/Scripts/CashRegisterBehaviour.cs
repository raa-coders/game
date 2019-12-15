using System;
using Glasses;
using Orders;
using UnityEngine;

public class CashRegisterBehaviour : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLLISION");
        
        if (!other.CompareTag("GlassInterior"))
            return;

        Debug.LogWarning("Cashing glass!");
        
        OrdersCanvas canvas = FindObjectOfType<OrdersCanvas>();
        GlassInteriorBehaviour glass = other.GetComponent<GlassInteriorBehaviour>();

        Order order = new Order();
        if (!canvas.Peek(ref order))
            return;
        
        float score = glass.Check(order);
        if (score > 0.6)
        {
            // Success
            canvas.DequeueOrder(true);
        }
        else
        {
            canvas.DequeueOrder(false);
        }

        glass.CleanUp();
    }
}