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

    public AudioSource WalkAS;
    public AudioSource JumpAS;
    
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
        GatherInput(); // Get player input
        Look(); // Rotate character to look in input direction
        Jump(); // Jump on input
    }

    private void FixedUpdate()
    {
        if (Look()){ // Check if character is ready to move (rotated correctly)
            Move();  // Move character in facing direction
        }
    }

    

    // --------------------------------------------------------------------------- //
    void GatherInput() // Retrieved players directional input, works with both controller and direction keys
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    bool Look()
    {
        if (_input != Vector3.zero)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0,45,0)); // A 4x4 rotation matrix that rotates 45 degrees around y axis 
            var skewedInput = matrix.MultiplyPoint3x4(_input);      // Rotate the given input direction
            
            var relative = (transform.position + skewedInput) - transform.position; // Give the relative look direction for the player object
            var rot = Quaternion.LookRotation(relative, Vector3.up); // Target player object rotation
            
            if (IsGrounded()) // Rotation interpolation when grounded
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnspeed * Time.deltaTime);
                return transform.rotation == rot; // If player is ready to move
            }
            else // Player rotation when in the air
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnspeedInAir * Time.deltaTime);
                return true;
            }
        }
        return false;
    }
    void Move() // Move character in look direction (slower if in the air)
    {
        if (IsGrounded()) {
            if (!WalkAS.isPlaying) WalkAS.Play();
            _rb.MovePosition(transform.position + 
                             transform.forward * (Mathf.Round(_input.magnitude) * _speed * Time.deltaTime));
        } else {
            _rb.MovePosition(transform.position +
                             transform.forward * (Mathf.Round(_input.magnitude) * _speedInAir * Time.deltaTime));   
        }
    }
    

    public void Jump() // Add jump force if space bar is pressed
    {
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            JumpAS.Play();
            _rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }
    }
 
   bool IsGrounded() // Checks if player is within a given distance directly above a ground object
   {
       return Physics.Raycast(transform.position, Vector3.down, _distToGround + 0.1f);
    }
}

