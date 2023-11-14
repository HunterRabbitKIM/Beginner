using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    public BGMList BGMList;
    private GameObject player;
    private bool isGameOver = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Enemy") && !isGameOver)
        {
            isGameOver = true;
            GameOver();
        }
    }

    public void GameOver()
    {
        Debug.Log("���� ���� �Լ� ȣ��!");
        GameOverScreen.Setup();
        //BGMList.PauseBGM();
        player.GetComponent<BGMList>().PlayBGM("GameOver"); // BGM ��� ������ �����մϴ�.
        Time.timeScale = 0;

        
    }
 
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
