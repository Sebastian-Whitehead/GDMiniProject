using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {
   //For at kunne hover og ændre materiale skal man have 2 materialer og tilføje dem til sit objekt.
   public Material original; 
   public Material hovered;

   Renderer rend;

   private Copy_paste copy_Paste;

   void Start() {
      rend = GetComponent<Renderer>();
   }

   void Awake() {
      GameObject gameManager = GameObject.Find("Managers/GameManager");
      copy_Paste = gameManager.GetComponent<Copy_paste>();
   }

   void OnMouseEnter(){
      //Debug.Log("Enter");
      if (copy_Paste.pastes <= 0) return;
      if (gameObject.layer == LayerMask.NameToLayer("Ghost")) return;
      rend.material = hovered;

   }

   void OnMouseExit(){
      //Debug.Log("Exit");
      if (copy_Paste.pastes <= 0) return;
      if (gameObject.layer == LayerMask.NameToLayer("Ghost")) return;
      rend.material = original;
   }
}
