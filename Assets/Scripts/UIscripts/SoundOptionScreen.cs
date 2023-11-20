using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOptionScreen : MonoBehaviour
{
    public PauseScreen PauseScreen;
    public void Setup()
    {
        gameObject.SetActive(true);
    }
    public void ApplyButton()
    {
        gameObject.SetActive(false);
        PauseScreen.Setup();
    }

    
}
