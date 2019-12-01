using System;
using UnityEngine;

namespace Glasses
{
    public class GlassInteriorBehaviour : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "liquid")
            {
                Debug.Log("LIQUID COLLISION!!");
                Destroy(other.gameObject);
            }
        }
    }
}
