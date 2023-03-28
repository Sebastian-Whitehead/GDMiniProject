using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{

    public float speed;
    public Rigidbody rb;
    public bool playerIsOnGround = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float vertical = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
      

        transform.Translate(horizontal, 0, vertical);

        if(Input.GetButtonDown("Jump") && playerIsOnGround)
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            playerIsOnGround = false;
        }
    }

    private void OncollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            playerIsOnGround = true;
        }
    }
}
