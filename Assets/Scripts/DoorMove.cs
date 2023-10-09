using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour
{
    public bool open = false;
    public float doorOpenAngle = 90f; //�� ���� �� ����
    public float doorCloseAngle = 0f; //�� ���� �� ����
    public float smoot = 2f; //���� ���� ������ �ε巯�� ����

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
    
    private void OnTriggerEnter(Collider other)
    {
        IHandle item = other.GetComponent<IHandle>();
        if (item!=null&&!item.myName().Equals(""))
        {
            if (item.myName().Equals("key"))
            {
                ChangeDoorState();
                item.destroy();
            }
        }
    }
}
