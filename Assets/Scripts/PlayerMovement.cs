using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        // Rigidbody referance
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
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
        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(4);
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

    private bool IsGrounded() 
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
