using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed = 5.0f; // 걷기 속도
    public float runSpeed = 10.0f; // 달리기 속도

    public float mouseSensitivity = 2.0f; //마우스 감도
    public float verticalRotation = 0; //세로 회전 각도
    public float horizontalRotation; //전후 회전 각도

    public Transform playerCamera; //플레이어 카메라

    float moveSpeed; // 이동에 관한 변수(달리기 또는 걷기)
    float hAxis;
    float vAxis;
    float moveMagnitude; //이동 벡터 크기 변수

    //점프 변수
    public float jumpPower = 8.0f;
    public float gravity = -20f;
    public bool isJumping = false;

    private CharacterController characterController; // 캐릭터 컨트롤러

    Vector3 moveDirection; //이동 방향 벡터 변수

    public Animator animator; // 애니메이터

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>(); // 캐릭터 컨트롤러 컴포넌트 가져오기
    }

    // Update is called once per frame
    void Update()
    {

        Turn();
        Move();

        if(Input.GetButtonDown("Jump")&& !isJumping)
        {
            Debug.Log("Jump1");
            Jump();
        }
        
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
        //이동
        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        hAxis = Input.GetAxis("Horizontal") * moveSpeed; //좌우 이동 방향과 속도
        vAxis = Input.GetAxis("Vertical") * moveSpeed;  // 전후 이동 방향과 속도

        moveDirection = new Vector3(hAxis, 0, vAxis);
        moveDirection = transform.TransformDirection(moveDirection); //이동 방향을 로컬 좌표계로 변환

        //애니메이터 매개 변수 설정
        moveMagnitude = moveDirection.magnitude;
        animator.SetBool("isWalk", moveMagnitude > 0);
        animator.SetBool("isRun", moveMagnitude > 0 && Input.GetKey(KeyCode.LeftShift));

        // 이동 적용
        characterController.Move(moveDirection * Time.deltaTime); // 캐릭터 컨트롤러를 통해 이동 적용

        Debug.Log(characterController.isGrounded);
        //점프
        if (characterController.isGrounded == false)
        {
            //Debug.Log(characterController.isGrounded);
            moveDirection.y -= gravity * Time.deltaTime;
            Debug.Log(moveDirection.y);
        }
        else
        {
            isJumping = false;
        }
        
    }

    void Jump()
    {
        
        if(characterController.isGrounded)
        {
            Debug.Log("Jump2");
            moveDirection.y = jumpPower;
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }
    }

}
