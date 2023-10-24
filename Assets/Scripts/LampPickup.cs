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
                // 이미 램프를 들고 있다면 램프를 놓는다.
                DropLamp();
            }
            else
            {
                // 램프를 든다.
                PickUpLamp();
            }
        }

        if (isHolding)
        {
            // 램프를 플레이어의 위치와 방향에 따라 이동
            MoveLampWithPlayer();
        }
    }

    void PickUpLamp()
    {
        Ray ray = new Ray(playerTransform.position, playerTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5.0f)) // 2.0f는 Ray의 최대 거리
        {
            if (hit.collider.CompareTag("Lamp")) // 램프에 대한 태그를 사용
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
            // 램프를 플레이어 위치와 방향으로 이동
            heldLamp.transform.position = playerTransform.position + playerTransform.forward * 2.0f;
        }
    }
}
