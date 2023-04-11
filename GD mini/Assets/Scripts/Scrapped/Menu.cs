using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public AudioSource RestartAS;

    void Update() {
        Debug.Log(gameObject.name);
        if (Input.GetKeyDown(KeyCode.R)) ReloadScene();
    }

    public void ReloadScene() {
        StartCoroutine(ReloadSceneSound());
    }

    IEnumerator ReloadSceneSound(){
        RestartAS.Play();
        yield return new WaitWhile (()=> RestartAS.isPlaying);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
     }
}
