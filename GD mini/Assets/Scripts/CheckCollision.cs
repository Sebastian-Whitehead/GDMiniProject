using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour {

    public bool colliding;

    private AudioSource audioSource;
    private Rigidbody rb;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        float speed = new Vector2(rb.velocity.x, rb.velocity.z).magnitude;

        if (speed > 0.01f && !audioSource.isPlaying) {
            audioSource.Play();
        } else {
            audioSource.Stop();
        }
    }
    
    void OnTriggerStay(Collider col) {
        colliding = true;
    }

    void OnTriggerExit(Collider col) { 
        colliding = false;
    }
}