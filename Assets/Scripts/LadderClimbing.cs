using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimbing : MonoBehaviour
{
    public float climbingSpeed = 2.0f;
    public bool isClimbing = false;

    private CharacterController characterController;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleClimbing();
    }

    private void HandleClimbing()
    {
        // ��ٸ��� �������� ���¿��� �̵��� ó���ϴ� �ڵ�
        float verticalInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y;
        float horizontalInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0);

        // �̵� ����� ȸ���� �����Ѵ�.
        moveDirection = transform.TransformDirection(moveDirection) * climbingSpeed;

        // CharcterController�� ����Ͽ� �̵�
        characterController.Move(moveDirection * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            // ��ٸ� Ʈ���ſ� �����ϸ� �������� ���·� ��ȯ�մϴ�.
            isClimbing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            // ��ٸ� Ʈ���ſ��� ������ �������� ���¸� �����մϴ�.
            isClimbing = false;
        }
    }
}
