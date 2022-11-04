using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpspeed;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    

    private void Awake()
    {
        // Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput < -0.01f && !isLeftWalled()) // If player is moving left, face left
        {
            body.velocity = new Vector2(horizontalInput * speed,body.velocity.y); // Gets x axis input from player
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput > 0.01f && !isRightWalled()) // If player is facing right, face right
        {
            body.velocity = new Vector2(horizontalInput * speed,body.velocity.y); // Gets x axis input from player
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if(Input.GetKey(KeyCode.Space) && isGrounded()) // Gets jump input from player
        {
            Jump();
        }

        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpspeed);
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.01f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool isLeftWalled()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, 0.01f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool isRightWalled()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, 0.01f, groundLayer);
        return raycastHit.collider != null;
    }
}
