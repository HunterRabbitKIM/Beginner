using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [SerializeField] private float lookSensitivity;
    [SerializeField] private float cameraRotationLimit;
    [SerializeField] private float interactDistance;
    [SerializeField] private float currentCameraRotationX;

    [SerializeField] private Camera theCamera;
    [SerializeField] private Rigidbody myRigid;

    private float moveSpeed;
    private float hAxis;
    private float vAxis;

    [SerializeField] private float jumpPower;
    [SerializeField] private float gravity;
    private bool isRun = false;

    private CharacterController characterController;

    private AudioSource audioSource;
    public AudioClip[] floorRunSounds;
    private bool isPlayingRunSound = false;

    Vector3 moveDirection;


    void Awake()
    {
        myRigid = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        TryRun();
        Move();
        CameraRotation();
        CharacterRotation();
        DoorOpen();
        UpdateRunSound();
    }
    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;

        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
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
    void UpdateRunSound()
    {
        if (isRun)
        {
            if (!isPlayingRunSound)
            {
                audioSource.loop = true;
                CheckGroundTagAndPlaySound();
                isPlayingRunSound = true;
            }
        }
        else
        {
            if (isPlayingRunSound)
            {
                StopRunSound();
                isPlayingRunSound = false;
            }
        }
    }
    public void RunSound(AudioClip soundClip)
    {
        
        audioSource.clip = soundClip;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void StopRunSound()
    {
        audioSource.Stop();
    }

    void CheckGroundTagAndPlaySound()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10.0f))
        {
            Debug.DrawRay(transform.position, Vector3.down * 10.0f, Color.red, 1.0f);
            string groundTag = hit.collider.tag;
            Debug.Log("Ground Tag: " + groundTag);

            if (isRun)
            {
                // 바닥 태그에 따라 다른 소리를 재생
                int soundIndex = GetSoundIndexByGroundTag(groundTag);
                if (soundIndex != -1)
                {
                    RunSound(floorRunSounds[soundIndex]);
                }
            }
        }
    }

    int GetSoundIndexByGroundTag(string groundTag)
    {
        // 바닥 태그에 따라 해당하는 소리의 인덱스를 반환
        for (int i = 0; i < floorRunSounds.Length; i++)
        {
            if (groundTag.Equals(floorRunSounds[i].name))
            {
                return i;
            }
        }

        // 매칭되는 소리가 없는 경우
        return -1;
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
