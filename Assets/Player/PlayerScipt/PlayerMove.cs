using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public Rigidbody2D rb;

    [HideInInspector] public Transform groundCheck;
    [HideInInspector] public LayerMask groundLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
//======================================
    public void Move(float horizontal, float speed)
    {
        rb.linearVelocityX = (horizontal * speed);
    }

    public void Jump(float jumpForce)
    {
        rb.linearVelocityY = (jumpForce);

    }
	
    

    public bool isGrounded()
    {
        return Physics2D.OverlapCapsule
                    (
                        groundCheck.position,
                        new Vector2(1f, 0.25f),
                        CapsuleDirection2D.Horizontal,
                        0,
                        groundLayer
                    );
    }



}
