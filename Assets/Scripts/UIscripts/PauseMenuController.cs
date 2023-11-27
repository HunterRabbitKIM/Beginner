using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public SoundManager SoundManager;
    public PauseScreen PauseScreen;
    public SoundOptionScreen SoundOptionScreen;

    private enum ActiveMenu
    {
        None,
        Pause,
        SoundOption
    }

    private ActiveMenu currentMenu = ActiveMenu.None;

    private void Update()
    {
        if (currentMenu == ActiveMenu.None)
        {
            PauseButton();
        }

    }
    private void PauseButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || OVRInput.Get(OVRInput.Button.Three))
        {
            Pause();
        }
    }
    public void Pause()
    {
        PauseScreen.Setup();
        SoundManager.PauseBGM();
        Time.timeScale = 0;
        currentMenu = ActiveMenu.Pause;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        SoundManager.ResumeBGM();
        currentMenu = ActiveMenu.None;
    }
    public void ShowSoundOptions()
    {
        SoundOptionScreen.Setup();
        currentMenu = ActiveMenu.SoundOption;
    }

}