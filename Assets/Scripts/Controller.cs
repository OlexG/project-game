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

    bool getIsTouchingLeftWall(Transform pos) {
        return Physics2D.Linecast(pos.position,
            transform.position + Vector3.left * 0.7f, 1 << LayerMask.NameToLayer("Ground"));
    }
    bool getIsTouchingRightWall(Transform pos) {
        return Physics2D.Linecast(pos.position,
            transform.position + Vector3.right * 0.7f, 1 << LayerMask.NameToLayer("Ground"));
    }

    bool getIsTouchingWall(Transform pos) {
        return getIsTouchingLeftWall(pos) || getIsTouchingRightWall(pos);
    }

    bool getIsTouchingGround(Transform pos) {
        return Physics2D.Linecast(pos.position,
            transform.position + Vector3.down * 0.7f, 1 << LayerMask.NameToLayer("Ground"));
    }

    // Update is called once per frame
    void Update()
    {

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        

        // translate object
        Vector2 movement = new Vector2(moveHorizontal, 0.0f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(movement.x * speed, GetComponent<Rigidbody2D>().velocity.y);     

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        isGrounded = getIsTouchingGround(lowerLeftCorner) || getIsTouchingGround(lowerRightCorner);
        if (isGrounded) {
            wallJumpCount = 0;
        }

        // check if object is on wall
        bool isTouchingWall = getIsTouchingWall(upperLeftCorner) && getIsTouchingWall(upperRightCorner) ||
            getIsTouchingWall(lowerLeftCorner) && getIsTouchingWall(lowerRightCorner);


        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && wallJumpCount < wallJumpCountLimit && isTouchingWall)
        {
            // set vertical velocity to 0
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0.0f);
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            wallJumpCount++;
            Debug.Log(wallJumpCount);
        }
    }
}
