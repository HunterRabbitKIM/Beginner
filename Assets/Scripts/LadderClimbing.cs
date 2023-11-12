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
        // 사다리를 기어오르기 상태에서 이동을 처리하는 코드
        float verticalInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y;
        float horizontalInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0);

        // 이동 방향과 회전을 조절한다.
        moveDirection = transform.TransformDirection(moveDirection) * climbingSpeed;

        // CharcterController를 사용하여 이동
        characterController.Move(moveDirection * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            // 사다리 트리거에 진입하면 기어오르기 상태로 전환합니다.
            isClimbing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            // 사다리 트리거에서 나오면 기어오르기 상태를 해제합니다.
            isClimbing = false;
        }
    }
}
