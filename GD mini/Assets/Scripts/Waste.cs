using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waste : MonoBehaviour {

    public AudioSource WasteAS;

    void OnTriggerEnter(Collider collider) {
        if (collider.transform.tag != "Player") return;
        StartCoroutine(WasteRestart());
    }
    
    IEnumerator WasteRestart(){
        WasteAS.Play();
        yield return new WaitWhile (()=> WasteAS.isPlaying);
        new UI().ReloadScene();
     }
}
