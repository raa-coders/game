using System;
using UnityEngine;
using Random = System.Random;

namespace Bottles
{
    public class BottleBehaviour : MonoBehaviour
    {
        public GameObject LiquidSpawnRoot;

        public LiquidBehaviour LiquidPrefab;
        
        // TODO temporary way to rotate the bottle
        public float Rotation = 0;
        
        public void Update()
        {
            Vector3 rot = Vector3.zero;
            rot.y = 0; // Freeze y rotation
            rot.z = Rotation;

            SpawnLiquid(rot.z);
            
            transform.localEulerAngles = rot;
        }



        public void SpawnLiquid(float rotation)
        {
            int minRot = 60;

            if (Math.Abs(rotation) < minRot)
                return;
            
            Vector3 pos = LiquidSpawnRoot.transform.position;

            int amount = 1 + Mathf.FloorToInt(UnityEngine.Random.value * 2);
            for (int i = 0; i < amount; i++)
            {
                LiquidBehaviour liquid = Instantiate(LiquidPrefab);
                liquid.transform.position = pos;
            }
        }
    }
}