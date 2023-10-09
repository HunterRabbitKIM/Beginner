using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewAngle : MonoBehaviour
{
    public float viewAngle; // 시야각
    public float viewDistance;//시야거리
    public LayerMask targetMask;  // 타겟 마스크

    private Monster monster;
    private Player thePlayer;

    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
        monster = GetComponentInParent<Monster>();
        StartCoroutine("Viewing");
    }
    IEnumerator Viewing()
    {
        yield return new WaitForSeconds(0.05f);
        View();
        StartCoroutine("Viewing");
    }
    void Update()
    {
       
    }

    public Vector3 GetTargetPos()
    {
        return thePlayer.transform.position;
    }

    private Vector3 BoundaryAngle(float angle)
    {
        angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    public bool View()
    {
        Vector3 _leftBoundary = BoundaryAngle(-viewAngle * 0.5f);
        Vector3 _rightBoundary = BoundaryAngle(-viewAngle * 0.5f);

        Debug.DrawRay(transform.position + transform.up, _leftBoundary, Color.red);
        Debug.DrawRay(transform.position + transform.up, _rightBoundary, Color.red);

        Collider[] target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        for(int i=0; i< target.Length; i++)
        {
            Transform targetTrans = target[i].transform;
            if(targetTrans.tag == "Player")
            {
                Vector3 direaction = (targetTrans.position - transform.position).normalized;
                float angle = Vector3.Angle(direaction, transform.forward);

                if (angle < viewAngle * 0.5f)
                {
                    RaycastHit hit;
                    if(Physics.Raycast(transform.position + transform.up, direaction,out hit,viewDistance))
                    {
                        if(hit.transform.tag == "Player")
                        {
                            Debug.Log("플레이어가 시야 내에 있습니다.");
                            Debug.DrawRay(transform.position + transform.up, direaction, Color.blue);

                            return true;
                        }
                    }
                }
            }
        }
    return false;
    }
   
}
