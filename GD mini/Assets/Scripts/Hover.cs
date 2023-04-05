using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
//For at kunne hover og ændre materiale skal man have 2 materialer og tilføje dem til sit objekt.
   public Material original; 
   public Material hovered;

   Renderer rend;
       void Start()
    {
        rend = GetComponent<Renderer>();

    }

   void OnMouseEnter(){
      Debug.Log("Enter");
      rend.material = hovered;      
   }

   void OnMouseExit(){
    Debug.Log("Exit");
    rend.material = original;
   }
}
