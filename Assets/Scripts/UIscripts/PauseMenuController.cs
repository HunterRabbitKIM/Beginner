using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public SoundManager SoundManager;
    public PauseScreen PauseScreen;


    private void PauseButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {   
            Pause();
        }
    }
    public void Pause()
    {
        PauseScreen.Setup();
        SoundManager.PauseBGM();
        Time.timeScale = 0;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PauseButton();
    }
}
