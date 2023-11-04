using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    public BGMList BGMList;
    


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Enemy"))
        {
            GameOver();
            
        }
    }

    public void GameOver()
    {
        GameOverScreen.Setup();
        BGMList.PauseBGM();
        Time.timeScale = 0;
       


    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
