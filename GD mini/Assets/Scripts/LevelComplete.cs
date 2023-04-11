using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour {

    public string nextLevelNr;

    public AudioSource MeowAS;

    private void OnTriggerEnter(Collider col) {
        if (col.tag != "Player") return;
        StartCoroutine(PlayEndSceneSound());
    }

    IEnumerator PlayEndSceneSound(){
        MeowAS.Play();
        yield return new WaitWhile (()=> MeowAS.isPlaying);
        string nextLevel = "Level " + nextLevelNr;
        SceneManager.LoadScene(nextLevel);
     }
}
