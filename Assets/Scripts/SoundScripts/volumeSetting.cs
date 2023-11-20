using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class volumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider BgmSlider;
    [SerializeField] private Slider SfxSlider;

    private void start()
    {
        if (PlayerPrefs.HasKey("BgmVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetBGMVolume();
            SetSFXVolume();
        }
    }
   
    public void SetBGMVolume()
    {
        float volume = BgmSlider.value;
        myMixer.SetFloat("BGM", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("BgmVolume", volume);
    }
    public void SetSFXVolume()
    {
        float volume = SfxSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }

    private void LoadVolume()
    {
        BgmSlider.value = PlayerPrefs.GetFloat("BgmVolume");
        SfxSlider.value = PlayerPrefs.GetFloat("SfxVolume");

        SetBGMVolume();
        SetSFXVolume();
    }
}
