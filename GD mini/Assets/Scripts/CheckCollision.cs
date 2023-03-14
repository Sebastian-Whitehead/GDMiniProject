using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour {

    public bool colliding;
    
    void OnTriggerStay(Collider col) {
        colliding = true;
    }

    void OnTriggerExit(Collider col) { 
        colliding = false;
    }
}