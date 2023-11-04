using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("Stage2");
        Time.timeScale = 1;
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("Title");
        Time.timeScale = 1;
    }
    
}
