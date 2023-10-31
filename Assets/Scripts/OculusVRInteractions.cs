using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OculusSampleFramework; // Oculus Integration 패키지에서 필요한 네임스페이스를 가져옵니다.
using Oculus.Interaction.Input;

public class OculusVRInteractions : MonoBehaviour
{
    public GameObject uiObject; // 활성화할 UI 오브젝트
    public LayerMask interactableLayer; // 상호작용 가능한 레이어 마스크

    private bool isTouching = false; // 터치 중인지 여부
    public GameObject paper;

    private void Update()
    {
        CheckTouchInteraction(); // 터치 상호작용을 확인하는 함수 호출
    }

    private void CheckTouchInteraction()
    {
        // Oculus 터치 컨트롤러의 터치 입력을 확인합니다.
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
            // 터치 입력을 놓았을 때 UI를 비활성화합니다.
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