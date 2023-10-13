using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OculusSampleFramework; // Oculus Integration ��Ű������ �ʿ��� ���ӽ����̽��� �����ɴϴ�.
using Oculus.Interaction.Input;

public class OculusVRInteractions : MonoBehaviour
{
    public GameObject uiObject; // Ȱ��ȭ�� UI ������Ʈ
    public LayerMask interactableLayer; // ��ȣ�ۿ� ������ ���̾� ����ũ

    private bool isTouching = false; // ��ġ ������ ����

    private void Update()
    {
        CheckTouchInteraction(); // ��ġ ��ȣ�ۿ��� Ȯ���ϴ� �Լ� ȣ��
    }

    private void CheckTouchInteraction()
    {
        // Oculus ��ġ ��Ʈ�ѷ��� ��ġ �Է��� Ȯ���մϴ�.
        if (OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, OVRInput.Controller.LTouch) ||
            OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            if (!isTouching)
            {
                // ��Ʈ�ѷ����� ����ĳ��Ʈ�� �߻��Ͽ� ��ȣ�ۿ� ������ ��ü�� Ȯ���մϴ�.
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, interactableLayer))
                {
                    // ��ġ�ϰ� �ִ� ������Ʈ�� ��ȣ�ۿ� ������ ��� UI�� Ȱ��ȭ�մϴ�.
                    uiObject.SetActive(true);
                }
            }
            isTouching = true;
        }
        else
        {
            // ��ġ �Է��� ������ �� UI�� ��Ȱ��ȭ�մϴ�.
            isTouching = false;
            uiObject.SetActive(false);
        }
    }
}