using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTeleport : MonoBehaviour
{
    public GameObject monsterPrefab;  // ������ ������ ������
    public Transform monsterSpawnPoint;    // ���Ͱ� ������ ��ġ
    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasSpawned)
        {
            SpawnMonster();
            hasSpawned = true;
        }
    }

    private void SpawnMonster()
    {
        Instantiate(monsterPrefab, monsterSpawnPoint.position, monsterSpawnPoint.rotation);
        // ���� ���� �� �߰����� �����̳� �ִϸ��̼��� ������ �� �ֽ��ϴ�.
    }
}
