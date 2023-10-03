using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour
{
    public bool open = false;
    public float doorOpenAngle = 90f; //문 열릴 때 각도
    public float doorCloseAngle = 0f; //문 닫힐 때 각도
    public float smoot = 2f; //문이 열고 닫을때 부드러운 정도

    void Start()
    {
        
    }
    public void ChangeDoorState()
    {
        open = !open;
    }

    // Update is called once per frame
    void Update()
    {
        if(open)
        {
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoot * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smoot * Time.deltaTime);
        }
    }
}
