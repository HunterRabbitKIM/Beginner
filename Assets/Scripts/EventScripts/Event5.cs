using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event5 : MonoBehaviour
{
    public GameObject enemy;

    public GameObject target;

    private bool start=false;
    
    public GameObject Event6;

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            Vector3 direction = target.transform.position - enemy.transform.position;
            direction.Normalize();
            float speed = 4f;
            transform.Translate(direction * (speed * Time.deltaTime));
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            StartCoroutine(Eventroute());
        }
    }

    IEnumerator Eventroute()
    {
        enemy.GetComponent<Animator>().SetBool("JumpingDown",true);
        yield return new WaitForSeconds(0.4f);
        start = true;
        yield return new WaitForSeconds(0.4f);
        enemy.gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1f);
        enemy.gameObject.SetActive(false);
        Event6.gameObject.SetActive(true);
        Destroy(gameObject);
    }
    
}
