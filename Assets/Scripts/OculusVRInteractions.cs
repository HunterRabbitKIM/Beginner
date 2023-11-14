using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusVRInteractions : MonoBehaviour
{
    public GameObject[] uiObject; // 활성화 할 UI 오브젝트
    public LayerMask interactableLayer; // 상호작용 가능한 레이어 마스크
    public GameObject paper;

    public int arraySize;

    private bool isTouching = false; // 터치 중인지 여부

    // Update is called once per frame
    void Start()
    {
        arraySize = 7;
        uiObject = new GameObject[arraySize];
    }

    private void CheckTouchInteraction()
    {
        if(OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            if(!isTouching)
            {
                if(paper != null)
                {
                    if(paper.name == "Paper_1")
                    {
                        isTouching = true;
                        uiObject[0].SetActive(true);
                    }
                    else if(paper.name == "Paper_2")
                    {
                        isTouching = true;
                        uiObject[1].SetActive(true);
                    }
                    else if (paper.name == "Paper_3")
                    {
                        isTouching = true;
                        uiObject[2].SetActive(true);
                    }
                    else if (paper.name == "Paper_4")
                    {
                        isTouching = true;
                        uiObject[3].SetActive(true);
                    }
                    else if (paper.name == "Paper_5")
                    {
                        isTouching = true;
                        uiObject[4].SetActive(true);
                    }
                    else if (paper.name == "Paper_6")
                    {
                        isTouching = true;
                        uiObject[5].SetActive(true);
                    }
                    else if (paper.name == "Paper_7")
                    {
                        isTouching = true;
                        uiObject[6].SetActive(true);
                    }
                }
            }
        }
        else
        {
            isTouching = false;
            for(int i = 0; i < arraySize; i++)
            {
                uiObject[i].SetActive(false);
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Paper"))
        {
            for(int i = 0; i < arraySize; i++)
            {
                paper = other.gameObject;
            }
        }
    }
}
