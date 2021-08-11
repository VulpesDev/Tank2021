using System.Collections;
using UnityEngine;

public class DestroyTime : MonoBehaviour
{
    [SerializeField] float timeInSecs;
    private void Awake()
    {
        StartCoroutine(Dest());
    }
    IEnumerator Dest()
    {
        yield return new WaitForSeconds(timeInSecs);
        Destroy(gameObject);
    }
}
