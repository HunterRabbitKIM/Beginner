using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnTrigger : MonoBehaviour
{
    public AudioClip triggerSound;

    private AudioSource audioSource;
    private bool hasEnteredTrigger = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� �÷��̾��̰� Ʈ���ſ� ���� �������� ���� ���
        if (other.CompareTag("Player") && !hasEnteredTrigger)
        {
            Debug.Log("Ʈ���� ����");

            // ������ ���带 ���
            audioSource.PlayOneShot(triggerSound);

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
