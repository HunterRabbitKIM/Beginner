using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event7 : MonoBehaviour
{
    [Multiline (3)]
    public string text;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            UIManager.instance.ShowplayerLine(text);
            Destroy(gameObject);
        }
    }
}
