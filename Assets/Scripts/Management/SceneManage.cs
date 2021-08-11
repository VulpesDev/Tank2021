using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneManage : MonoBehaviour
{
    static public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    static public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    static public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    static public void Quit()
    {
        Application.Quit();
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Tutorial_2")
        {
            if(GameObject.FindGameObjectWithTag("Enemy") == null && GameObject.Find("Portal(Clone)") == null)
            {
                Instantiate(Resources.Load<GameObject>("Portal"));
            }
        }

    }
}
