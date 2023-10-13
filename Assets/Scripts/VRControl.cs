using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.UI;
using OVR;

public class VRControl : OVRGrabbable
{
    public GameObject uiObject; // UI GameObject ����

    private bool isUIVisible = false;

    private void Awake()
    {
        uiObject.SetActive(false);
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        Debug.Log("OK");

        // ���� ������Ʈ�� �׷����� �� ������ �ڵ� �߰�
        // ���� ������Ʈ�� ��ȣ �ۿ� ���� ���·� ����
        // UI�� Ȱ��ȭ�ϰų� �ٸ� ���� ����

        if (!isUIVisible)
        {
            Debug.Log(isUIVisible);
            uiObject.SetActive(true); // UI Ȱ��ȭ
            isUIVisible = true;
        }
    }

    // OVRGrabbable���� ��ӵ� OnGrabEnd �޼��带 ������
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);

        // �׷��� �������� �� ������ �ڵ� �߰�
        // ���� ������Ʈ���� ��ȣ �ۿ� ����

        if (isUIVisible)
        {
            uiObject.SetActive(false); // UI ��Ȱ��ȭ
            isUIVisible = false;
        }
    }
}
