using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnTrigger : MonoBehaviour
{
    public AudioClip triggerSound;
    private bool hasEnteredTrigger = false;
    SoundManager soundManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� �÷��̾��̰� Ʈ���ſ� ���� �������� ���� ���
        if (other.CompareTag("Player") && !hasEnteredTrigger)
        {
            Debug.Log("Ʈ���� ����");

            // ������ ���带 ���
            soundManager.PlaySFX(triggerSound);

            // ���尡 �� �̻� ������� �ʵ��� �÷��׸� true�� ����
            hasEnteredTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �浹�� ��ü�� �÷��̾��� ���
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ʈ���� ����");
        }
    }
}
