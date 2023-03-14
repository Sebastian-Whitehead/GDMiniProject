using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnspeed = 720;
    
    [SerializeField] private float _speedInAir = 4;
    [SerializeField] private float _turnspeedInAir = 360;
    
    private Vector3 _input;
    private bool _playerIsOnGround = true;

    
    void Update()
    {
        GatherInput();
        Look();
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    

    // --------------------------------------------------------------------------- //
    void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    void Look()
    {
        if (_input != Vector3.zero)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0,45,0));
            var skewedInput = matrix.MultiplyPoint3x4(_input);
            
            var relative = (transform.position + _input) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            if (_playerIsOnGround)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnspeed * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnspeedInAir * Time.deltaTime);
            }
            

        }
    }
    void Move()
    {
        if(_playerIsOnGround){
            _rb.MovePosition(transform.position + (transform.forward * _input.magnitude) * _speed * Time.deltaTime);
        }else {
            _rb.MovePosition(transform.position + (transform.forward * _input.magnitude) * _speedInAir * Time.deltaTime);    
        }
    }
    

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && _playerIsOnGround)
        {
            _rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            _playerIsOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            _playerIsOnGround = true;
        }
    }
}

