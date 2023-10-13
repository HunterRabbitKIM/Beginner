using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.UI;
using OVR;

public class VRControl : OVRGrabbable
{
    public GameObject uiObject; // UI GameObject 설정

    private bool isUIVisible = false;

    private void Awake()
    {
        uiObject.SetActive(false);
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        Debug.Log("OK");

        // 종이 오브젝트를 그랩했을 때 실행할 코드 추가
        // 종이 오브젝트와 상호 작용 중인 상태로 변경
        // UI를 활성화하거나 다른 동작 수행

        if (!isUIVisible)
        {
            Debug.Log(isUIVisible);
            uiObject.SetActive(true); // UI 활성화
            isUIVisible = true;
        }
    }

    // OVRGrabbable에서 상속된 OnGrabEnd 메서드를 재정의
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);

        // 그랩을 해제했을 때 실행할 코드 추가
        // 종이 오브젝트와의 상호 작용 중지

        if (isUIVisible)
        {
            uiObject.SetActive(false); // UI 비활성화
            isUIVisible = false;
        }
    }
}
