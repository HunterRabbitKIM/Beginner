using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusVRInteractions : MonoBehaviour
{
    public GameObject uiObject; // Ȱ��ȭ �� UI ������Ʈ
    public LayerMask interactableLayer; // ��ȣ�ۿ� ������ ���̾� ����ũ
    public GameObject paper;

    private bool isTouching = false; // ��ġ ������ ����

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckTouchInteraction()
    {
        if(OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            if(!isTouching)
            {
                Debug.Log(paper.name);
                if(paper != null)
                {
                    if(paper.name == "Paper")
                    {
                        isTouching = true;
                        uiObject.SetActive(true);
                    }
                }
            }
        }
        else
        {
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
