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
    public GameObject uIHelpers;
    public GameObject controllerRight;
    public GameObject customHandLeft;
    public GameObject customHandRight;

    public void Setup()
    {
        gameObject.SetActive(true);
        uIHelpers.SetActive(true);
        controllerRight.SetActive(true);
        customHandLeft.SetActive(false);
        customHandRight.SetActive(false);
    }
    public void ResumeButton()
    {
        PauseMenuController.ResumeGame();
        gameObject.SetActive(false);
        uIHelpers.SetActive(false);
        controllerRight.SetActive(false);
        customHandLeft.SetActive(true);
        customHandRight.SetActive(true);
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