using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {


    public AudioSource RestartAS;

    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) ReloadScene();
    }

    public void ReloadScene() {
        StartCoroutine(ReloadSceneSound());
    }

    public IEnumerator ReloadSceneSound(){
        Debug.Log("Reload scene");
        RestartAS.Play();
        yield return new WaitWhile (()=> RestartAS.isPlaying);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
     }
}
