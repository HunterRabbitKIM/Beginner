using System;
using System.Collections;
using System.Collections.Generic;
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
            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other==presser)
        {
            button.transform.localPosition = new Vector3(0, 0.03f, 0);
            onRelease.Invoke();
            isPressed = false;
        }
    }

    public void setevent()
    {
        UIManager.instance.ShowplayerLine("문이 열리는 소리가 들린다.");
        door.GetComponent<XRGrabInteractable>().enabled = true;
    }
}
