using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class PauseScreen : MonoBehaviour
{
    public PauseMenuController PauseMenuController;
    public SoundManager SoundManager;
    public SoundOptionScreen SoundOptionScreen;

    public void Setup()
    {
        gameObject.SetActive(true);
    }
    public void ResumeButton()
    {
        PauseMenuController.ResumeGame();
        gameObject.SetActive(false);
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("Title");
        Time.timeScale = 1;
    }
    public void SoundOptionButton()
    {
        PauseMenuController.ShowSoundOptions();
        gameObject.SetActive(false);
    }

}