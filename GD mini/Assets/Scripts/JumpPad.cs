using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float launchForce = 10;
    
    private Rigidbody _playerRb;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        _playerRb = player.GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding with object");
        if(collision.gameObject != player) return;
        _playerRb.AddForce(new Vector3(0, launchForce, 0), ForceMode.Impulse);
    }
}
