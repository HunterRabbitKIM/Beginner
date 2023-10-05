using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed = 5.0f; // ??? ???
    public float runSpeed = 10.0f; // ????? ???

    public float mouseSensitivity = 2.0f; //???²J ????
    public float verticalRotation = 0; //???? ??? ????
    public float horizontalRotation; //???? ??? ????

    public Transform playerCamera; //?¡À???? ????

    float moveSpeed; // ????? ???? ????(????? ??? ???)
    float hAxis;
    float vAxis;
    float moveMagnitude; //??? ???? ??? ????

    private CharacterController characterController; // ©¦???? ??????

    Vector3 moveDirection; //??? ???? ???? ????

    public Animator animator; // ????????
    

    public float interactDiastance = 5f; //????????? ?????? ?????? ???

    
    // Start is called before the first frame update
    void Start()
    { 
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>(); // ©¦???? ?????? ??????? ????????
    }

    // Update is called once per frame
    void Update()
    {
        //???
        horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0); //?¡À???? ???????? ???

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90, 90); // ???? ??? ???? ????
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0); // ???? ??? ????

        //???
        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        hAxis = Input.GetAxisRaw("Horizontal") * moveSpeed; //?¢¯? ??? ????? ???
        vAxis = Input.GetAxisRaw("Vertical") * moveSpeed;  // ???? ??? ????? ???
        
        moveDirection = new Vector3(hAxis, 0, vAxis);
        moveDirection = transform.TransformDirection(moveDirection); //??? ?????? ???? ?????? ???

        //???????? ??? ???? ????
        moveMagnitude = moveDirection.magnitude;
        animator.SetBool("isWalk", moveMagnitude > 0);
        animator.SetBool("isRun", moveMagnitude > 0 && Input.GetKey(KeyCode.LeftShift));

        // ??? ????
        characterController.Move(moveDirection * Time.deltaTime); // ©¦???? ???????? ???? ??? ????

        Ray ray = new Ray(transform.position, transform.forward); //???? ??????? ??????
        RaycastHit hit;
        //?? ???? ??? Key:E
        if (Physics.Raycast(ray, out hit, interactDiastance))
            {
                if (hit.collider.CompareTag("Door")){
                    if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.GetComponent<DoorMove>().ChangeDoorState();
                }  
                }
            }    
    }
}
