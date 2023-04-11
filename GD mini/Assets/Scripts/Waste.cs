using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waste : MonoBehaviour {
    void OnTriggerStay(Collider collider) {
        if (collider.transform.tag != "Player") return;
        new Menu().ReloadScene();
    }
}
