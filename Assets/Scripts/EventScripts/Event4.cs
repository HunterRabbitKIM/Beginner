using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event4 : MonoBehaviour
{
    public GameObject enemy;

    public GameObject Event5;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    private void OnEnable()
    {
        enemy.GetComponent<Animator>().SetBool("Connerlooking",true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            Event5.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
