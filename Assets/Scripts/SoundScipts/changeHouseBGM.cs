using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeHouseBGM : MonoBehaviour
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
            player.GetComponent<BGMList>().PlayBGM("House"); // BGM ��� ������ �����մϴ�.
        }
        else if (distanceToPlayer > triggerDistance && isPlayerInsideTrigger)
        {
            player.GetComponent<BGMList>().PlayBGM("Forest"); // BGM ��� ������ �����մϴ�.
            isPlayerInsideTrigger = false;
            // ���ϴ� ���, �÷��̾ ������ ������ �� ������ �ڵ� �߰� ����
        }
        //Debug.Log(distanceToPlayer);

    }
}
