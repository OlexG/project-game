using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 10.0f;

    private bool isGrounded = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        // translate object
        Vector2 movement = new Vector2(moveHorizontal, 0.0f);
        transform.Translate(movement * speed);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
        // check if object is grounded
        isGrounded = Physics2D.Linecast(transform.position,
            transform.position + Vector3.down * 0.7f, 1 << LayerMask.NameToLayer("Ground"));
        
    }
}
