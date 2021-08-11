using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 5;
    float speed_base;
    Transform shootPoint;
    public bool ammo = true, destruct = false;
    private bool canDash;
    [SerializeField]TrailRenderer trail;

    [SerializeField] Animator anime_volumes;

    private void Awake()
    {
        speed_base = speed;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shootPoint = transform.GetChild(3);
    }

    void Update()
    {
        Movement();
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }
        if (canDash && Input.GetKeyDown(KeyCode.LeftShift)) { StartCoroutine(Dash()); }
        if (CanShoot() && Input.GetButtonDown("Fire1") && ammo) Shoot();
    }

    bool CanShoot()
    {
        if(GameObject.FindGameObjectWithTag("PBullet") == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Shoot()
    {
        Instantiate(Resources.Load<GameObject>("Shoot1"));
        Instantiate(Resources.Load<GameObject>("PBullet"), shootPoint.position, transform.rotation);
        //Play sound
    }

    Vector2 movement;
    int diagonal;
    void Movement()
    {
        float con = 0.2f;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        diagonal = horizontal * vertical != 0 ? 0 : 1;
        movement = new Vector2(horizontal, vertical);

        if (horizontal != 0 || vertical != 0)
        {
            //is moving
            if (vertical > con)
            {
                //Move up
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 180f);
            }
            else if (vertical < -con)
            {
                //Move down
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
            }
            if (horizontal > con)
            {
                //Move right
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90f);
            }
            else if (horizontal < -con)
            {
                //Move left
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -90f);
            }

            rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        }
    }

    IEnumerator Dash()
    {
        Instantiate(Resources.Load<GameObject>("Whoosh"));
        anime_volumes.SetBool("Dash", true);
        yield return new WaitForSeconds(0.01f);
        anime_volumes.SetBool("Dash", false);
        trail.emitting = true;
        speed = speed_base * 4;
        destruct = true;
        yield return new WaitForSeconds(0.2f);
        destruct = false;
        speed = speed_base;
        trail.emitting = false;
        rb.velocity = -transform.up * speed;

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement* diagonal * speed * Time.fixedDeltaTime);
    }
    void Die()
    {
        SceneManage.ReloadScene();
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EBullet")) { Die(); }
    }
}
