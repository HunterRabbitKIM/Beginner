using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            LoadingSceneController.LoadScene(sceneToLoad);
        }
    }
}
