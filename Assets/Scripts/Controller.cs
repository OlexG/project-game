using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 10.0f;
    public int wallJumpCountLimit = 1;

    private bool isGrounded = true;
    private int wallJumpCount = 0;
    
    public Transform upperLeftCorner;
    public Transform upperRightCorner;
    public Transform lowerLeftCorner;
    public Transform lowerRightCorner;

    bool getIsTouchingWall(Transform pos) {
        return Physics2D.Linecast(pos.position,
            transform.position + Vector3.right * 0.2f, 1 << LayerMask.NameToLayer("Ground")) || 
            Physics2D.Linecast(transform.position,
            transform.position + Vector3.left * 0.2f, 1 << LayerMask.NameToLayer("Ground"));
    }

    bool getIsTouchingGround(Transform pos) {
        return Physics2D.Linecast(pos.position,
            transform.position + Vector3.down * 0.7f, 1 << LayerMask.NameToLayer("Ground"));
    }

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
        isGrounded = getIsTouchingGround(lowerLeftCorner) || getIsTouchingGround(lowerRightCorner);
        if (isGrounded) {
            wallJumpCount = 0;
        }

        // check if object is on wall
        bool isTouchingWall = getIsTouchingWall(upperLeftCorner) || getIsTouchingWall(upperRightCorner) ||
            getIsTouchingWall(lowerLeftCorner) || getIsTouchingWall(lowerRightCorner);

        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && wallJumpCount < wallJumpCountLimit && isTouchingWall)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            wallJumpCount++;
        }
    }
}
