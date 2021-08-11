using UnityEngine;

public class destParts : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("PBullet") || collision.CompareTag("EBullet"))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerS;
            playerS = collision.gameObject.GetComponent<Player>();
            if (playerS.destruct)
            {
                //GetComponent<Rigidbody2D>().isKinematic = false;
                //GetComponent<Collider2D>().isTrigger = true;
                playerS.speed -= 1;
                GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CamBeahviour>().ShortShake();
                Instantiate(Resources.Load<GameObject>("Snap"));
                Destroy(gameObject);
            }
        }
    }
}
