using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waste : MonoBehaviour {

    public AudioSource WasteAS;

    void OnTriggerStay(Collider collider) {
        if (collider.transform.tag != "Player") return;
    }
    
    IEnumerator PlayEndSceneSound(){
        WasteAS.Play();
        yield return new WaitWhile (()=> WasteAS.isPlaying);
        new Menu().ReloadScene();
     }
}
