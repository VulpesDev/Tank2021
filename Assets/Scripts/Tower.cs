using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    [Header("UI Components")]
    public GameObject panelG;
    public GameObject panelR;
    public GameObject hp1, hp2, hp3;
    //NOT USED
    public TextMeshProUGUI towerText;
    //NOT USED

    public TextMeshProUGUI sceneTxt;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTr = player.transform;
        pScript = player.GetComponent<Player>();
        layerMask = 1 << LayerMask.NameToLayer("PRaycast");
        lr = transform.parent.GetChild(1).GetComponent<LineRenderer>();
        if(sceneTxt.text != SceneManager.GetActiveScene().name)
        {
            sceneTxt.text = SceneManager.GetActiveScene().name;
        }
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

        UpdateHPUI();

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
    void UpdateHPUI()
    {
        //towerText.text = "Tower's HP: "+HP;
        if(HP==2)
        {
            hp3.SetActive(false);
        }
        else if (HP == 1)
        {
            hp2.SetActive(false);
            hp3.SetActive(false);
        }
        else if (HP <= 0)
        {
            hp1.SetActive(false);
            hp2.SetActive(false);
            hp3.SetActive(false);
        }
        else
        {
            hp1.SetActive(true);
            hp2.SetActive(true);
            hp3.SetActive(true);
        }
    }
}
