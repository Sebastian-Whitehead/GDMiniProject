using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Waste : MonoBehaviour {

    public AudioSource WasteAS;

    void OnTriggerEnter(Collider collider) {
        if (collider.transform.tag != "Player") return;
        StartCoroutine(WasteRestart());
    }
    
    IEnumerator WasteRestart(){
        WasteAS.Play();
        while (WasteAS.isPlaying) yield return null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
