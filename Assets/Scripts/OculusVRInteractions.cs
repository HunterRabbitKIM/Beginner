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
    public GameObject paper;

    private void Update()
    {
        CheckTouchInteraction(); // ��ġ ��ȣ�ۿ��� Ȯ���ϴ� �Լ� ȣ��
    }

    private void CheckTouchInteraction()
    {
        // Oculus ��ġ ��Ʈ�ѷ��� ��ġ �Է��� Ȯ���մϴ�.
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            if (!isTouching)
            {
                Debug.Log(paper.name);
                if(paper != null)
                {
                    if(paper.name == "Paper")
                    {
                        Debug.Log("Paper");
                        isTouching = true;
                        uiObject.SetActive(true);
                    }
                }
            }
        }
        else
        {
            // ��ġ �Է��� ������ �� UI�� ��Ȱ��ȭ�մϴ�.
            isTouching = false;
            uiObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Paper"))
        {
            paper = other.gameObject;
        }
    }

    
}