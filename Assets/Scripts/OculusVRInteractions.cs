using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusVRInteractions : MonoBehaviour
{
    public GameObject uiObject; // 활성화 할 UI 오브젝트
    public LayerMask interactableLayer; // 상호작용 가능한 레이어 마스크
    public GameObject paper;

    private bool isTouching = false; // 터치 중인지 여부

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
