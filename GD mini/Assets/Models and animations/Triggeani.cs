using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggeani : MonoBehaviour
{
    Animator anim;
    private CharacterController _controller;
    private float _speed = 5f;
    private float _speed1 = 5f;
    //public AudioClip impact;
    //AudioSource audioSource;
    bool isMoving = false;
 
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponent<Animator>();
       // audioSource = GetComponent<AudioSource>();
        
    }
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        if (Input.GetKey("d") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("w"))
        {
            anim.SetTrigger("Move");
        }


        

        if (isMoving) {
           // while (audioSource.isPlaying == false)
           {
           // audioSource.Play();
        }

        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction1 = new Vector3(0, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        Vector3 velocity1 = direction1 * _speed1;

        //_controller.Move(velocity * Time.deltaTime);
        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
        anim.SetFloat("Speed1", Mathf.Abs(verticalInput));
        if (velocity.x != 0 || velocity1.z != 0){
            isMoving = true;
        }
        else {
            isMoving = false;
            //audioSource.Stop();
        }
    }
}
