using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject player;

    // Start is called before the first frame update
    void Start() {
        player.transform.position = spawnPoint.transform.position;

    }

    // Update is called once per frame
    void Update() {
        
    }
}
