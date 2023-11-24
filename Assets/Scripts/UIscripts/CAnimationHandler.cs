using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CAnimationHandler : MonoBehaviour
{
    #region Fields
    #endregion Fields

    #region Members
    private Animator m_Animator;

    #endregion Members


    #region Methods
    void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void EnterNextScene()
    {
        
        m_Animator.Play("Ending");
    }
    public void OnEnterNextSceneWithDelay()
    {
        StartCoroutine(LoadTitleSceneAfterDelay(10f));
    }

    IEnumerator LoadTitleSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene("Title");
    }

    #endregion Methods


}
