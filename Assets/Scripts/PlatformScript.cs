using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player") animator.SetBool("isLanded", true);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        animator.SetBool("isLanded", false);
    }
}
