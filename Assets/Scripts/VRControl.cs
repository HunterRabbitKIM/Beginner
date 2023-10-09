using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class VRControl : MonoBehaviour
{
    float MoveSpeed = 10f;
    CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 mov2d = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector3 mov = new Vector3(mov2d.x * Time.deltaTime * MoveSpeed, 0f, mov2d.y * Time.deltaTime * MoveSpeed);
        characterController.Move(mov);
    }
}
