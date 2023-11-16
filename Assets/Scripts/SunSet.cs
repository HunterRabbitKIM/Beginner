using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSet : MonoBehaviour
{
    public float rotationSpeed; // 해의 회전 속도
    public float time; // 해가 지는 각도

    void Update()
    {
        // 해를 주어진 속도로 회전
        transform.Rotate(new Vector3(10,0,0) * (rotationSpeed * Time.deltaTime));

        // 설정된 지점에 도달하면 오브젝트 비활성화
        if (Time.time>time)
        {
            gameObject.SetActive(false);
        }
    }
}
