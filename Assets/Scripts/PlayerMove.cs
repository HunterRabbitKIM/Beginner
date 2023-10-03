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

    private CharacterController characterController; // ĳ���� ��Ʈ�ѷ�

    Vector3 moveDirection; //�̵� ���� ���� ����

    public Animator animator; // �ִϸ�����
    

    public float interactDiastance = 5f; //������Ʈ�� ��ȣ�ۿ� ������ �Ÿ�

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>(); // ĳ���� ��Ʈ�ѷ� ������Ʈ ��������
    }

    // Update is called once per frame
    void Update()
    {
        //ȸ��
        horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0); //�÷��̾ �������� ȸ��

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90, 90); // ���� ȸ�� ���� ����
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0); // ī�޶� ȸ�� ����

        //�̵�
        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        hAxis = Input.GetAxisRaw("Horizontal") * moveSpeed; //�¿� �̵� ����� �ӵ�
        vAxis = Input.GetAxisRaw("Vertical") * moveSpeed;  // ���� �̵� ����� �ӵ�
        
        moveDirection = new Vector3(hAxis, 0, vAxis);
        moveDirection = transform.TransformDirection(moveDirection); //�̵� ������ ���� ��ǥ��� ��ȯ

        //�ִϸ����� �Ű� ���� ����
        moveMagnitude = moveDirection.magnitude;
        animator.SetBool("isWalk", moveMagnitude > 0);
        animator.SetBool("isRun", moveMagnitude > 0 && Input.GetKey(KeyCode.LeftShift));

        // �̵� ����
        characterController.Move(moveDirection * Time.deltaTime); // ĳ���� ��Ʈ�ѷ��� ���� �̵� ����

        Ray ray = new Ray(transform.position, transform.forward); //���� ������Ʈ ��ȣ�ۿ�
        RaycastHit hit;
        //�� ���� �ݱ� Key:E
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
