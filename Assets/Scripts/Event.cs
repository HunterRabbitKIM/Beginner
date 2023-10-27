using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    public string text;

    public void OnTriggerEnter(Collider other)
    {
        UIManager.instance.ShowplayerLine(text);
        Destroy(gameObject);
    }
}
