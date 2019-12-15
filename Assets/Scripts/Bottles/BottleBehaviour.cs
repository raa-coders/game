using System;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Random = System.Random;

namespace Bottles
{
    public class BottleBehaviour : GrabableBehaviour
    {

        public Color LiquidColor;

        public Material BottleMaterial;
        
        public GameObject LiquidSpawnRoot;

        public LiquidBehaviour LiquidPrefab;
        
        public float Rotation = 0;



        public void Start()
        {
            MeshRenderer renderer = this.GetComponent<MeshRenderer>();
            renderer.material = new Material(BottleMaterial);
            renderer.material.SetColor("_BaseColor", this.LiquidColor);
        }
     
        
        
        public void Update()
        {
            base.Update();

            // Only pour when grabbed
            if (!_isGrabbed)
                return;
            
            
            Rotation = _mainCamera.transform.localEulerAngles.z;
            if (Rotation > 180)
                Rotation = Rotation - 360;
            
            Rotation *= -5;
            if (Rotation < -180)
                Rotation = -180;
            if (Rotation > 180)
                Rotation = 180;
            
            Vector3 rot = Vector3.zero;
            rot.y = 0; // Freeze y rotation
            rot.z = Rotation;
            
            SpawnLiquid(rot.z);
            
            transform.localEulerAngles = rot;
        }



        public void SpawnLiquid(float rotation)
        {
            int minRot = 45; // 45 so it starts pouting around 70 degrees

            if (Math.Abs(rotation) < minRot)
                return;

            float ratio = (Math.Abs(rotation) - minRot) / (180f - minRot);

            float minAmount = 4 * ratio;
            float extra = 5 * ratio * ratio;
            
            int amount = Mathf.FloorToInt(minAmount + UnityEngine.Random.value * extra);
            
            Vector3 pos = LiquidSpawnRoot.transform.position;
            for (int i = 0; i < amount; i++)
            {
                LiquidBehaviour liquid = Instantiate(LiquidPrefab);
                liquid.Init(this.LiquidColor);
                liquid.transform.position = pos 
                            + new Vector3(
                                -0.02f + UnityEngine.Random.value * 0.04f,
                                -0.02f + UnityEngine.Random.value * 0.04f,
                                -0.02f + UnityEngine.Random.value * 0.04f
                            );
}
        }
    }
}