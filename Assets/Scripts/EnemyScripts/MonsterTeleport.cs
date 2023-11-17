using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTeleport : MonoBehaviour
{
    public GameObject monsterPrefab;  // 등장할 몬스터의 프리팹
    public Transform monsterSpawnPoint;    // 몬스터가 등장할 위치
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
        // 몬스터 등장 후 추가적인 설정이나 애니메이션을 적용할 수 있습니다.
    }
}
