using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 moveInput;
    private Vector2 dashInput;
    private Vector3 baseScale;

    private float dashTimer;
    private float dashCDTimer;
    public bool isDashing;

    public float moveSpeed;
    public float dashMod;
    public float dashTime;
    public float dashCD;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
    }
    void Update()
    {
        if (!isDashing) rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocityY);
        else
        {
            rb.linearVelocity = new Vector2(dashInput.x * (moveSpeed + dashMod), 0);
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                dashCDTimer = dashCD;
                isDashing = false;
            }
        }
        if (dashCDTimer > 0) dashCDTimer -= Time.deltaTime;
        else dashCDTimer = 0;

        if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-baseScale.x, baseScale.y, baseScale.z);
        }
        if (moveInput.x > 0)
        {
            transform.localScale = baseScale;
        }
    }
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    public void OnSprint(InputValue value)
    {
        if (dashCDTimer != 0 || isDashing) return;
        isDashing = true;
        dashTimer = dashTime;
        if (moveInput.x != 0) dashInput = moveInput;
        else dashInput = new Vector2 (Mathf.Sign(transform.localScale.x), 0);
    }

}


