using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [SerializeField] private float lookSensitivity;

    [SerializeField] private float cameraRotationLimit;
    [SerializeField] private float currentCameraRotationX;

    [SerializeField] private float interactDistance; //문 상호작용 거리

    [SerializeField] private Camera theCamera;
    [SerializeField] private Rigidbody myRigid;

    private float moveSpeed; 
    private float hAxis; 
    private float vAxis; 

    [SerializeField] private float jumpPower;
    [SerializeField] private float gravity;
    private bool isRun = false;

    private CharacterController characterController;

    
    Vector3 moveDirection;

    void Awake()
    {
        myRigid = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        
    }

    void Update()
    {
        TryRun();
        Move();
        CameraRotation();       
        CharacterRotation();    
        DoorOpen();
    }

    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;

        // currentCameraRotationX += _cameraRotationX;  마우스 Y 반전
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
    private void CharacterRotation()  // 좌우 캐릭터 회전
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY)); // 쿼터니언 * 쿼터니언

        // Debug.Log(myRigid.rotation);  // 쿼터니언
        // Debug.Log(myRigid.rotation.eulerAngles); // 벡터
    }

    void Move()
    {
        if (characterController.isGrounded)
        {
            hAxis = Input.GetAxis("Horizontal") * moveSpeed;
            vAxis = Input.GetAxis("Vertical") * moveSpeed; 

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

    public bool GetRun()
    {
        return isRun;
    }

    void TryRun()
    {
        if (characterController.isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isRun = true;
                moveSpeed = runSpeed; 
            }
            else
            {
                isRun = false;
                moveSpeed = walkSpeed;
            }

            hAxis = Input.GetAxis("Horizontal") * moveSpeed;
            vAxis = Input.GetAxis("Vertical") * moveSpeed; 

            moveDirection = new Vector3(hAxis, 0, vAxis);
            moveDirection = transform.TransformDirection(moveDirection);

            moveDirection.y += gravity * Time.deltaTime;
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    void DoorOpen()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.collider.CompareTag("Door"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.GetComponent<DoorMove>().ChangeDoorState();
                    Debug.Log("E");
                }
            }
        }
    }
}
