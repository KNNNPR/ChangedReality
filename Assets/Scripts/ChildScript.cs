using UnityEngine;

public class ChildScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distanceToPlayer = player.position.x - transform.position.x;

        Vector2 direction = new Vector2(player.position.x - transform.position.x, 0).normalized * speed;
        rb.linearVelocity = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Platform") Destroy(collision.gameObject);
        if (collision.gameObject.name == "wall_1__00_1") Destroy(collision.gameObject);
    }
}
