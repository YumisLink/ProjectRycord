using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class escToMain : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Before");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ppt()
    {
        SceneManager.LoadScene("PPT");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MainMune");
    }
}
