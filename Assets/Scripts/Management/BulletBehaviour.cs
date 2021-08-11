using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    int bulletSpeed = 45;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        rb.MovePosition(new Vector3(rb.position.x, rb.position.y, 0) + -transform.up * bulletSpeed * Time.fixedDeltaTime);
    }
    void Die()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Die();
    }
}
