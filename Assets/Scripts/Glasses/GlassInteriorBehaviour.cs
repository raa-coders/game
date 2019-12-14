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
                GetComponent<Renderer>().material.color = new Color
                (UnityEngine.Random.Range(-10.0f, 10.0f), 
                UnityEngine.Random.Range(-10.0f, 10.0f),
                UnityEngine.Random.Range(-10.0f, 10.0f),
                1.0f);

            }
        }
    }
}
