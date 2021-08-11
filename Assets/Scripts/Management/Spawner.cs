using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    static int endNum = 10;
    static TextMeshProUGUI txt;
    static public int total;
    public int maxNum = 3;
    private void Awake()
    {
        total = 0;
        StartCoroutine(Spawn());
        txt = GameObject.FindGameObjectWithTag("Txt").GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        txt.text = (endNum - total).ToString();
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(2.0f, 8.0f));
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxNum && total < endNum)
        {
            Instantiate(Resources.Load<GameObject>("Enemy"), transform.position, Quaternion.identity);
        }
        else if(total >=endNum)
        {
            Instantiate(Resources.Load<GameObject>("Portal"));
        }
        StartCoroutine(Spawn());
    }
}
