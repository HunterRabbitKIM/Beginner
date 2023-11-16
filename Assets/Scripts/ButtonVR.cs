using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonVR : MonoBehaviour
{
    public GameObject button;

    public UnityEvent onPress;

    public UnityEvent onRelease;

    private GameObject presser;
    AudioSource sound;
    private bool isPressed;

    public GameObject door;
    public GameObject door1;
    public GameObject EnemyEvent1;
    public GameObject Sound;
    
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0.003f, 0);
            sound.Play();
            isPressed = true;
            Debug.Log("버튼 이벤트 실행");
            UIManager.instance.ShowplayerLine("문이 열리는 소리가 들린다.");
            door1.gameObject.SetActive(false);
            door.gameObject.SetActive(true);
            EnemyEvent1.gameObject.SetActive(true);
            Sound.GetComponent<AudioSource>().Play();
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (other==presser)
        {
            button.transform.localPosition = new Vector3(0, 0.03f, 0);
            onRelease.Invoke();
            isPressed = false;
        }
    }*/
    
}
