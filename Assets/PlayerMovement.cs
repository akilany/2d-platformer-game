using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    // Start is called before the first frame update
    private void Start()
    {
        // Rigidbody referance
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Get Horizontal movement if > 0 move right, < 0 move left
        dirX = Input.GetAxisRaw("Horizontal");
        // Adjust velocity based on dirX value - or + (Change X, Keep Y)
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        // If press space JUMP (Keep X, Change Y)
        if (Input.GetButtonDown("Jump")) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (dirX > 0f) 
        {
            anim.SetBool("running", true);
            sprite.flipX = false;
        } 
        else if (dirX < 0f) 
        {
            anim.SetBool("running", true);
            sprite.flipX = true;
        }
        else 
        {
            anim.SetBool("running", false);
        }
    }
}