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

    private void Update()
    {
        CheckTouchInteraction(); // 터치 상호작용을 확인하는 함수 호출
    }

    private void CheckTouchInteraction()
    {
        // Oculus 터치 컨트롤러의 터치 입력을 확인합니다.
        if (OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, OVRInput.Controller.LTouch) ||
            OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            if (!isTouching)
            {
                // 컨트롤러에서 레이캐스트를 발사하여 상호작용 가능한 물체를 확인합니다.
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, interactableLayer))
                {
                    // 터치하고 있는 오브젝트가 상호작용 가능한 경우 UI를 활성화합니다.
                    uiObject.SetActive(true);
                }
            }
            isTouching = true;
        }
        else
        {
            // 터치 입력을 놓았을 때 UI를 비활성화합니다.
            isTouching = false;
            uiObject.SetActive(false);
        }
    }
}