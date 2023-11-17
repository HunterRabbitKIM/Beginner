using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmOptionSceen : MonoBehaviour
{
    public AudioSource bgmSource;

    public void SetBgmVolume(float volume)
    {
        bgmSource.volume = volume;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
