using UnityEngine;

public class PlayerMovementLogic : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private float jumpPower = 12f;

    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float coyoteTimeCounter;

    [SerializeField] private float jumpBufferTime = 0.2f;
    [SerializeField] private float jumpBufferTimeCounter;

    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Animator animator;

    private float currentSpeed = 0f;
    private bool isGravityInverted = false;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("speed", Mathf.Abs(horizontal));

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            animator.SetBool("isGrounded", true);
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
            animator.SetBool("isGrounded", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferTimeCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferTimeCounter -= Time.deltaTime;
        }

        if (jumpBufferTimeCounter > 0f && coyoteTimeCounter > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, isGravityInverted ? -jumpPower : jumpPower);
            jumpBufferTimeCounter = 0f;
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }

        // Инверсия гравитации по нажатию E
        if (Input.GetKeyDown(KeyCode.E))
        {
            isGravityInverted = !isGravityInverted;
            rb.gravityScale *= -1f; // Инвертируем гравитацию

            // Переворачиваем персонажа вверх ногами
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (horizontal != 0)
            currentSpeed = Mathf.MoveTowards(currentSpeed, horizontal * speed, acceleration * Time.fixedDeltaTime);
        else
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundChecker.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
