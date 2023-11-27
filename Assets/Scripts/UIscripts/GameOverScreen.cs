using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public SoundManager soundManager;
    private GameObject gameOver;

    private void Update()
    {
        gameOver.GetComponent<SoundManager>().PlayBGM("GameOver");
        Time.timeScale = 0;
    }


    public void Setup()
    {
        gameObject.SetActive(true);
    }
    public void RestartButton()
    {
        LoadingSceneController.LoadScene("Stage2");
        Time.timeScale = 1;
    }
    public void ExitButton()
    {
        LoadingSceneController.LoadScene("Title");
        Time.timeScale = 1;
    }
    
}
