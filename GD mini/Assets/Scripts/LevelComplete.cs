using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour {

    public string nextLevelNr;

    private void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            string nextLevel = "Level " + nextLevelNr;
            SceneManager.LoadScene(nextLevel);
        }
    }
}
