using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
        }
    }


    private void Update()
    {

    }

    public void StartBtn()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void Option()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Sound()
    {

    }
    
}
