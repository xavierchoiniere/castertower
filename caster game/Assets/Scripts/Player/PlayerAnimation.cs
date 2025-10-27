using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerJump playerJump;
    private PlayerMovement playerMovement;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerJump = GetComponent<PlayerJump>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if (playerJump.isGrounded) animator.SetInteger("Jump", 0);
        else animator.SetInteger("Jump", Mathf.RoundToInt(playerJump.rb.linearVelocityY));
        
        animator.SetBool("Run", playerMovement.moveInput.x != 0);
        animator.SetBool("Dash", playerMovement.isDashing);
    }
}
