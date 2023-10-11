using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 5.0f ;
    public float runSpeed = 15.0f;
    private float applySpeed;

    public float mouseSensitivity = 2.0f;
    public float verticalRotation = 0;
    public float horizontalRotation;
    public float interactDiastance = 5f;

    public Transform playerCamera;

    float moveSpeed;
    float hAxis;
    float vAxis;

    public float jumpPower = 8.0f;
    public float gravity = -20f;
    private bool isRun = false;

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
        TryRun();
        Turn();
        Move();
        DoorOpen();
    }

    void Turn()
    {
        //회전
        horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0); //플레이어를 수평으로 회전

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90, 90); // 상하 회전 각도 제한
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0, 0); // 카메라 회전 설정
    }

    void Move()
    {

        if (characterController.isGrounded)
        {
            hAxis = Input.GetAxis("Horizontal") * walkSpeed;
            vAxis = Input.GetAxis("Vertical") * walkSpeed;

            moveDirection = new Vector3(hAxis, 0, vAxis);
            moveDirection = transform.TransformDirection(moveDirection);

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpPower;
            }
        }
        moveDirection.y += gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }
    void DoorOpen()
    {
        Ray ray = new Ray(transform.position, transform.forward); //???? ??????? ??????
        RaycastHit hit;
        //?? ???? ??? Key:E
        if (Physics.Raycast(ray, out hit, interactDiastance))
        {
            if (hit.collider.CompareTag("Door"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.GetComponent<DoorMove>().ChangeDoorState();
                }
            }
        }
    }

    public bool GetRun()
    {
        return isRun;
    }

    private void TryRun()
    {
        if (characterController.isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isRun = true;

                hAxis = Input.GetAxis("Horizontal") * runSpeed;
                vAxis = Input.GetAxis("Vertical") * runSpeed;

                moveDirection = new Vector3(hAxis, 0, vAxis);
                moveDirection = transform.TransformDirection(moveDirection);

                moveDirection.y += gravity * Time.deltaTime;
                characterController.Move(moveDirection * Time.deltaTime);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isRun = false;
                runSpeed = walkSpeed;
            }
        }
            
    }
}