using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    string[] direction = new string[4];
    bool canMove = true;

    Rigidbody2D rb;
    int speed = 20;
    private void Awake()
    {
        StartCoroutine(RandomTimeDirection());
        StartCoroutine(RandomMovementStop());
        StartCoroutine(RandomShoot());
    }
    void Start()
    {
        direction[0] = "up";
        direction[1] = "down";
        direction[2] = "right";
        direction[3] = "left";
        orientation = RandomDirection();
        rb = GetComponent<Rigidbody2D>();
        shootPoint = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        CheckRaycast();
        UpdateDirection();
    }
    private void FixedUpdate()
    {
        if (canMove) Move();
    }

    void Move()
    {
        rb.velocity = -transform.up * speed;
    }

    string RandomDirection()
    {
        int newDir = Random.Range(0, 4);
        while (direction[newDir] == orientation) { newDir = Random.Range(0, 4); }
        return direction[newDir];
    }

    string orientation;
    void UpdateDirection()
    {
        switch (orientation)
        {
            case "up":
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 180f);
                break;
            case "down":
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
                break;
            case "right":
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90f);
                break;
            case "left":
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -90f);
                break;
        }

    }
    Transform shootPoint;
    void Shoot()
    {
        Instantiate(Resources.Load<GameObject>("Shoot2"));
        Instantiate(Resources.Load<GameObject>("EBullet"), shootPoint.position, transform.rotation);
        //Play shoot sound
    }
    IEnumerator RandomShoot()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        Shoot();
        StartCoroutine(RandomShoot());
    }
    IEnumerator RandomMovementStop()
    {
        yield return new WaitForSeconds(Random.Range(2.0f, 8.0f));
        canMove = false;
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        canMove = true;
        StartCoroutine(RandomMovementStop());
    }
    IEnumerator RandomTimeDirection()
    {
        yield return new WaitForSeconds(Random.Range(1.5f, 5.0f));
        orientation = RandomDirection();
        StartCoroutine(RandomTimeDirection());
    }

    [SerializeField] float rayDist = 0.1f;
    void CheckRaycast()
    {

        Vector2 pos = new Vector2(transform.GetChild(0).position.x,
            transform.GetChild(0).position.y);
        Debug.DrawRay(pos, -transform.up * rayDist, Color.red);

        if (Physics2D.Raycast(pos, -transform.up, rayDist, 1 << LayerMask.NameToLayer("Wall")))
        {
            orientation = RandomDirection();
        }
    }

    void Die()
    {
        Instantiate(Resources.Load<GameObject>("Explosion 1"));
        Spawner.total++;
        Instantiate(Resources.Load("Explosion"), transform.position, Quaternion.Euler(0, 0, Random.Range(0, 361)));
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PBullet"))
        {
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Enemy"))
        {
            orientation = RandomDirection();
        }
    }
}
