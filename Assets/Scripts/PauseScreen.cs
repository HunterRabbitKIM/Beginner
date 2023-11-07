using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class PauseScreen : MonoBehaviour
{

    public void Setup()
    {
        gameObject.SetActive(true);
    }
    public void ResumeButton()
    {
        Debug.Log("키눌림");
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("Title");
        Time.timeScale = 1;
    }
    public void OptionButton()
    {
        Debug.Log("미구현");
    }

}
