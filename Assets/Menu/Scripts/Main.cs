using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    void Start()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Level_1()
    {
        SceneManager.LoadScene(1);
    }

    public void Level_2()
    {
        SceneManager.LoadScene(2);
    }

    public void Level_3()
    {
        SceneManager.LoadScene(3);
    }

    public void ReturnMain()
    {
        SceneManager.LoadScene(3);
    }
}
