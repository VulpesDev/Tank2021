using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalPurpose : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            SceneManage.LoadNextScene();
        }
    }
}
