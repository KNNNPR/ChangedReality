using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementLogic : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // �������� �����
    private float horizontal;
    [SerializeField] private AudioClip walkSound; // ���� �����
    [SerializeField] private AudioClip jumpSound; // ���� ������
    [SerializeField] private GameObject pauseMenuUI; // ������ �� ������ �����
    private bool isPaused = false;
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

    [SerializeField]private bool gravitySphereCollected = false;

    [SerializeField] private bool isDoppel;
    private bool isWalking = false;
    private void Start()
    {
        if (isDoppel)
        {
            rb.gravityScale *= -1f;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ������� Esc
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0 && IsGrounded() && !isWalking)
        {
            isWalking = true;
            PlayLoopSound(walkSound); // ��������� ���� �����
        }
        else if (horizontal == 0 || !IsGrounded())
        {
            isWalking = false;
            audioSource.Stop(); // ������������� ���� �����
        }
        animator.SetFloat("speed", Mathf.Abs(horizontal));

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            animator.SetBool("isGrounded", true);
            StopLoop(jumpSound);
            
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
            animator.SetBool("isGrounded", false);
            PlayLoopSound(jumpSound);
            
         
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
            if (isDoppel) rb.linearVelocity = new Vector2(rb.linearVelocity.x, isGravityInverted ? jumpPower : -jumpPower);
            else rb.linearVelocity = new Vector2(rb.linearVelocity.x, isGravityInverted ? -jumpPower : jumpPower);
            jumpBufferTimeCounter = 0f;
        }

        if (Input.GetKeyUp(KeyCode.Space) && Mathf.Abs(rb.linearVelocity.y) > 0)//Cheking
        {
            
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            coyoteTimeCounter = 0f;
            
        }

        if (Input.GetKeyDown(KeyCode.Q) && gravitySphereCollected)
        {
            isGravityInverted = !isGravityInverted;
            rb.gravityScale *= -1f;

            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
        }

        Flip();
    }
    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private void PlayLoopSound(AudioClip clip)
    {
        if (clip != null && !audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
    private void StopLoop(AudioClip clip)
    {
        if (clip != null && !audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.loop = false;
            audioSource.Stop();
        }
    }
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true); // ���������� ����
        Time.timeScale = 0f; // ������������� ����
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // �������� ����
        Time.timeScale = 1f; // ������������ ����
        isPaused = false;
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // ��������������� ����� ����� �������
        SceneManager.LoadScene("MainMenu"); // �������� �������� ����
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Gravity_Sphere")
        {
            gravitySphereCollected = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.layer == 7)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameObject.SetActive(false);

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
