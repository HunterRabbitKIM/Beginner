using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMonsterBGM : MonoBehaviour
{
    public string bgmName = "";
    private GameObject player;
    [SerializeField] public float triggerDistance;
    private bool isPlayerInsideTrigger;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        isPlayerInsideTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        changeMusic();
    }

    void changeMusic()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= triggerDistance && !isPlayerInsideTrigger)
        {
            isPlayerInsideTrigger = true;
            player.GetComponent<BGMList>().PlayBGM("Monster"); // BGM 재생 로직을 실행합니다.
        }
        else if (distanceToPlayer > triggerDistance && isPlayerInsideTrigger)
        {
            player.GetComponent<BGMList>().PlayBGM("Default"); // BGM 재생 로직을 실행합니다.
            isPlayerInsideTrigger = false;
            // 원하는 경우, 플레이어가 범위를 나갔을 때 실행할 코드 추가 가능
        }
        //Debug.Log(distanceToPlayer);

    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            player.GetComponent<BGMList>().PlayBGM(bgmName);
            isPlayerInsideTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")
        {
            player.GetComponent<BGMList>().PlayBGM(bgmName);
            isPlayerInsideTrigger = false;
        }
    }*/
}
