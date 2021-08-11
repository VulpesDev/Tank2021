using UnityEngine;

public class Tower : MonoBehaviour
{
    Vector3 lookDir;
    int lives = 3;
    Player pScript;
    GameObject player;
    Transform playerTr;
    LayerMask layerMask;
    LineRenderer lr;
    public int HP = 3;

    public GameObject panelG;
    public GameObject panelR;



    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTr = player.transform;
        pScript = player.GetComponent<Player>();
        layerMask = 1 << LayerMask.NameToLayer("PRaycast");
        lr = transform.parent.GetChild(1).GetComponent<LineRenderer>();
    }

    Vector3[] positions = new Vector3[2];

    void Die()
    {
        SceneManage.ReloadScene();
        Destroy(gameObject);
    }
    void TakeDamage(int amount)
    {
        HP -= amount;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EBullet"))
        {
            TakeDamage(1);
        }
    }

    void Update()
    {

        if (HP <= 0) Die();

        if (player == null) return;

        lookDir = playerTr.position - transform.position;
        lookDir = lookDir.normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDir, Vector2.Distance(transform.position,
            playerTr.position), ~layerMask);
        if(hit)
        {
            if(hit.collider.CompareTag("Player"))
            {
                Debug.DrawRay(transform.position, lookDir * Vector2.Distance(transform.position,
    playerTr.position), Color.green);
                pScript.ammo = true;
                panelG.SetActive(true);
                panelR.SetActive(false);

                positions[0] = Vector3.zero;
                positions[1] = Vector3.zero;
                lr.SetPositions(positions);
            }
            else
            {
                Debug.DrawRay(transform.position, lookDir * Vector2.Distance(transform.position,
    playerTr.position), Color.red);

                positions[0] = transform.position;
                positions[1] = hit.point;
                lr.SetPositions(positions);
                pScript.ammo = false;
                panelG.SetActive(false);
                panelR.SetActive(true);
            }
        }
    }
}
