using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event3 : MonoBehaviour
{
    public GameObject horrorpic;
    AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            horrorpic.gameObject.SetActive(true);
            horrorpic.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}
