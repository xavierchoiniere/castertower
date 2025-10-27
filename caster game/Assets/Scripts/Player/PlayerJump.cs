using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool isGrounded;
    private float groundCheckRadius;

    public float jumpForce;
    public Transform groundChecker;
    public LayerMask platformLayer;

    public float fallMul;

    public int maxJumpCharge;
    private int currentJumpCharge;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheckRadius = 0.1f;
        currentJumpCharge = maxJumpCharge;
    }
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckRadius, platformLayer);
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Platform"), rb.linearVelocityY > 0 ||
            (!isGrounded && rb.linearVelocityY <= 0f && !Physics2D.Raycast(groundChecker.position, Vector2.down, 0.6f, platformLayer)));
        if (rb.linearVelocityY < 0) rb.linearVelocity += Vector2.up * Physics2D.gravity.y * fallMul * Time.deltaTime;
        if (isGrounded && rb.linearVelocityY == 0 && currentJumpCharge < maxJumpCharge) currentJumpCharge = maxJumpCharge;
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && (isGrounded || currentJumpCharge > 0))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            currentJumpCharge--;
        }
    }
}
