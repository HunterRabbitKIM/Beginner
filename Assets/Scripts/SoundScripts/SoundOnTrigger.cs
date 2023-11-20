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
        // 충돌한 객체가 플레이어이고 트리거에 아직 진입하지 않은 경우
        if (other.CompareTag("Player") && !hasEnteredTrigger)
        {
            Debug.Log("트리거 진입");

            // 지정된 사운드를 재생
            soundManager.PlaySFX(triggerSound);

            // 사운드가 더 이상 재생되지 않도록 플래그를 true로 설정
            hasEnteredTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 충돌한 객체가 플레이어인 경우
        if (other.CompareTag("Player"))
        {
            Debug.Log("트리거 나감");
        }
    }
}
