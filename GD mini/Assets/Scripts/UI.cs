using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            Debug.Log("Space is pressed");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
}
