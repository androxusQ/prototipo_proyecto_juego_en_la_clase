using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 6f;
    [SerializeField] float jumpForce = 12f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius = 0.15f;
    [SerializeField] LayerMask groundLayer;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    float moveX;
    bool grounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        grounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && grounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        if (moveX > 0) sr.flipX = false;
        else if (moveX < 0) sr.flipX = true;

        anim.SetFloat("Speed", Mathf.Abs(moveX));
        anim.SetFloat("VelocityY", rb.linearVelocity.y);
        anim.SetBool("Grounded", grounded);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);
    }
}