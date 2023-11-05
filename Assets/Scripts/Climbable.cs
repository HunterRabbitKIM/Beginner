using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbable : MonoBehaviour
{
    public float moveDistance = 5.0f; // 이동 거리
    public float moveSpeed = 2.0f;    // 이동 속도

    public GameObject TargetGameObject;

    private Vector3 targetPos;

    private string state;
    // Start is called before the first frame update
    void Start()
    {
        targetPos = TargetGameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            Vector3 initPos = other.transform.position;
            other.transform.position = Vector3.MoveTowards(initPos, initPos, moveSpeed * Time.deltaTime);
        }
    }
    
}
