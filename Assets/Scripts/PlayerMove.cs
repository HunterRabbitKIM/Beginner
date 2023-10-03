using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed = 5.0f; // �ȱ� �ӵ�
    public float runSpeed = 10.0f; // �޸��� �ӵ�

    public float mouseSensitivity = 2.0f; //���콺 ����
    public float verticalRotation = 0; //���� ȸ�� ����
    public float horizontalRotation; //���� ȸ�� ����

    public Transform playerCamera; //�÷��̾� ī�޶�

    float moveSpeed; // �̵��� ���� ����(�޸��� �Ǵ� �ȱ�)
    float hAxis;
    float vAxis;
    float moveMagnitude; //�̵� ���� ũ�� ����

    //���� ����
    public float jumpPower = 8.0f;
    public float gravity = -20f;
    public bool isJumping = false;

    private CharacterController characterController; // ĳ���� ��Ʈ�ѷ�

    Vector3 moveDirection; //�̵� ���� ���� ����

    public Animator animator; // �ִϸ�����

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>(); // ĳ���� ��Ʈ�ѷ� ������Ʈ ��������
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
        //ȸ��
        horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0); //�÷��̾ �������� ȸ��

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90, 90); // ���� ȸ�� ���� ����
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0, 0); // ī�޶� ȸ�� ����
    }

    void Move()
    {
        //�̵�
        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        hAxis = Input.GetAxis("Horizontal") * moveSpeed; //�¿� �̵� ����� �ӵ�
        vAxis = Input.GetAxis("Vertical") * moveSpeed;  // ���� �̵� ����� �ӵ�

        moveDirection = new Vector3(hAxis, 0, vAxis);
        moveDirection = transform.TransformDirection(moveDirection); //�̵� ������ ���� ��ǥ��� ��ȯ

        //�ִϸ����� �Ű� ���� ����
        moveMagnitude = moveDirection.magnitude;
        animator.SetBool("isWalk", moveMagnitude > 0);
        animator.SetBool("isRun", moveMagnitude > 0 && Input.GetKey(KeyCode.LeftShift));

        // �̵� ����
        characterController.Move(moveDirection * Time.deltaTime); // ĳ���� ��Ʈ�ѷ��� ���� �̵� ����

        Debug.Log(characterController.isGrounded);
        //����
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
