using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float runSpeed = 10.0f;

    public float mouseSensitivity = 2.0f;
    public float verticalRotation = 0;
    public float horizontalRotation;

    public Transform playerCamera;

    float moveSpeed;
    float hAxis;
    float vAxis;

    public float jumpPower = 8.0f;
    public float gravity = -20f;

    private CharacterController characterController;

    Vector3 moveDirection;

    // Start is called before the first frame update
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
        Move();
    }

    void Turn()
    {
        //ȸ��
        horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0); //�÷��̾ �������� ȸ��

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90, 90); // ���� ȸ�� ���� ����
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0, 0); // ī�޶� ȸ�� ����
    }

    void Move()
    {

        if(characterController.isGrounded)
        {
            moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
            hAxis = Input.GetAxis("Horizontal") * moveSpeed;
            vAxis = Input.GetAxis("Vertical") * moveSpeed;

            moveDirection = new Vector3(hAxis, 0, vAxis);
            moveDirection = transform.TransformDirection(moveDirection);

            if(Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpPower;
            }
        }
        moveDirection.y += gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
