using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCredit : MonoBehaviour
{
    public EndingScreen EndingScreen;
    public SoundManager SoundManager;
    public CharacterController playerController; 


    void Start()
    {
       
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndingFunction();
        }
    }

    public void EndingFunction()
    {
        if (playerController != null && playerController.enabled)
        {
            playerController.enabled = false;
        }

        EndingScreen.Setup();
        
        SoundManager.PlayBGM("Ending");


    }
  
}
