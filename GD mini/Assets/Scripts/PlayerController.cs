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

    private BoxCollider _playerCollider;
    // private bool _playerIsOnGround = true;
    private float _distToGround;

    void Start()
    {
        // get the distance to ground
        _distToGround = GetComponent<Collider>().bounds.extents.y;
        _playerCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        GatherInput();
        Look();
        Jump();
    }

    private void FixedUpdate()
    {
        if (Look()){ 
            Move();
        }
    }

    

    // --------------------------------------------------------------------------- //
    void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    bool Look()
    {
        if (_input != Vector3.zero)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0,45,0));
            var skewedInput = matrix.MultiplyPoint3x4(_input);
            
            var relative = (transform.position + skewedInput) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);
            
            if (IsGrounded())
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnspeed * Time.deltaTime);
                return transform.rotation == rot;
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnspeedInAir * Time.deltaTime);
                return true;
            }
        }
        return false;
    }
    void Move()
    {
        if (IsGrounded()) {
            _rb.MovePosition(transform.position + transform.forward * (Mathf.Round(_input.magnitude) * _speed * Time.deltaTime));
        } else {
            _rb.MovePosition(transform.position + transform.forward * (Mathf.Round(_input.magnitude) * _speedInAir * Time.deltaTime));    
        }
    }
    

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }
    }
 
   bool IsGrounded()
   {
       return Physics.Raycast(transform.position, Vector3.down, _distToGround + 0.1f);
    }
}

