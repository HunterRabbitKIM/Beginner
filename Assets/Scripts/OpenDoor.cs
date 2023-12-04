using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private float interactDistance;

    private void Update()
    {
        DoorOpen();
    }

    void DoorOpen()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.collider.CompareTag("Door"))
            {
                if (Input.GetKeyDown(KeyCode.E) || OVRInput.Get(OVRInput.Button.Four))
                {
                    hit.collider.GetComponent<DoorMove>().ChangeDoorState();
                    Debug.Log("E");
                }
            }
        }
    }
}
