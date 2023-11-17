using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class PauseScreen : MonoBehaviour
{
    private GameObject player;
    public BGMList BGMList;

    public void Setup()
    {
        gameObject.SetActive(true);
    }
    public void ResumeButton()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        BGMList.ResumeBGM();
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("Title");
        Time.timeScale = 1;
    }
    public void OptionButton()
    {
        Debug.Log("¹Ì±¸Çö");
    }

}
