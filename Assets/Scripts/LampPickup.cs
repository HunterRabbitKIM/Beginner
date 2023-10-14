using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampPickup : MonoBehaviour
{
    private bool isHolding = false;
    private GameObject heldLamp;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isHolding)
            {
                // �̹� ������ ��� �ִٸ� ������ ���´�.
                DropLamp();
            }
            else
            {
                // ������ ���.
                PickUpLamp();
            }
        }

        if (isHolding)
        {
            // ������ �÷��̾��� ��ġ�� ���⿡ ���� �̵�
            MoveLampWithPlayer();
        }
    }

    void PickUpLamp()
    {
        Ray ray = new Ray(playerTransform.position, playerTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5.0f)) // 2.0f�� Ray�� �ִ� �Ÿ�
        {
            if (hit.collider.CompareTag("Lamp")) // ������ ���� �±׸� ���
            {
                isHolding = true;
                heldLamp = hit.collider.gameObject;
            }
        }
    }

    void DropLamp()
    {
        isHolding = false;
        heldLamp = null;
    }

    void MoveLampWithPlayer()
    {
        if (heldLamp != null)
        {
            // ������ �÷��̾� ��ġ�� �������� �̵�
            heldLamp.transform.position = playerTransform.position + playerTransform.forward * 2.0f;
        }
    }
}
