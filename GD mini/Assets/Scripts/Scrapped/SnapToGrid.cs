using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector3(
            Mathf.Round(transform.position.x), 
            transform.position.y, 
            Mathf.Round(transform.position.z));
    }
}
