using System.Collections;
using Unity.VisualScripting;
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
    private PlayerAnimation playerAnimation;

    [SerializeField] private bool isDropping;
    private float dropTime = 0.15f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheckRadius = 0.1f;
        currentJumpCharge = maxJumpCharge;
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        if (rb.linearVelocityY < 0) playerAnimation.currentState = PlayerAnimation.PlayerState.Falling;
        if (isGrounded && rb.linearVelocityY == 0)
        {
            if (currentJumpCharge < maxJumpCharge) currentJumpCharge = maxJumpCharge;
            if (playerAnimation.currentState == PlayerAnimation.PlayerState.Falling) 
                playerAnimation.currentState = PlayerAnimation.PlayerState.Idle;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckRadius, platformLayer);
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Platform"), rb.linearVelocityY > 0 ||
            (!isGrounded && rb.linearVelocityY <= 0f && !Physics2D.Raycast(groundChecker.position, Vector2.down, 0.6f, platformLayer)) ||
            isDropping);
        if (rb.linearVelocityY < 0) rb.linearVelocity += Vector2.up * Physics2D.gravity.y * fallMul * Time.deltaTime;
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && (isGrounded || currentJumpCharge > 0))
        {
            playerAnimation.currentState = PlayerAnimation.PlayerState.Jumping;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            currentJumpCharge--;
        }
    }

    public void OnDrop(InputValue value)
    {
        if (value.isPressed && 
            isGrounded && 
            !Physics2D.OverlapCircle(groundChecker.position, groundCheckRadius, platformLayer).CompareTag("Main Floor")) 
            StartCoroutine(DropRoutine());
    }

    private IEnumerator DropRoutine()
    {
        isDropping = true;
        yield return new WaitForSeconds(dropTime);
        isDropping = false;
    }
}
