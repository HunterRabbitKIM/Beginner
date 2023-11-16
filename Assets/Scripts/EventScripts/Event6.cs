using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event6 : MonoBehaviour
{
    public GameObject enemy;

    public GameObject target;

    private bool start=false;

    public GameObject sound;

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            Vector3 direction = target.transform.position - enemy.transform.position;
            direction.Normalize();
            float speed = 10f;
            transform.Translate(direction * (speed * Time.deltaTime));
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            sound.GetComponent<AudioSource>().Stop();
            StartCoroutine(Eventroute());
        }
    }

    IEnumerator Eventroute()
    {
        enemy.GetComponent<Animator>().SetBool("Crawling",true);
        start = true;
        enemy.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(3.5f);
        enemy.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
