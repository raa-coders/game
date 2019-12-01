using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorOfLiquid : MonoBehaviour
{
    public GameObject ga;
    public GameObject liquid1;
    public Material green,black,red, white, blue;
    public Material[] material;
    Renderer rend;

    void Start()
    {
       green = (Material)Resources.Load("Resources/Glasses/LiquidMaterialGreen", typeof(Material));
       blue = (Material)Resources.Load("Resources/Glasses/LiquidMaterialBlue", typeof(Material));
       white = (Material)Resources.Load("Resources/Glasses/LiquidMaterialWhite", typeof(Material));
       black = (Material)Resources.Load("Resources/Glasses/LiquidMaterialBlack", typeof(Material));
       red = (Material)Resources.Load("Resources/Glasses/LiquidMaterialRed", typeof(Material));
       rend = GetComponent<Renderer>();
       rend.enabled = true;
       rend.sharedMaterial = material[0];
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        liquid1 = GameObject.Find("Liquid1");
        if(col.gameObject.tag == "Liquid1"){
           
           if(liquid1.GetComponent<Renderer>().material == green){
               if(rend.sharedMaterial == white){
               // liquid1.GetComponent<Renderer>().material = 
               }
               else if(rend.sharedMaterial == black){
                   
               }
               else if(rend.sharedMaterial == blue){
                   
               }
                else if(rend.sharedMaterial == green){
                   
               }
               else if(rend.sharedMaterial == red){
                   
               }
           }

        }

        ga.transform.SetParent(liquid1.transform);
    }
}
