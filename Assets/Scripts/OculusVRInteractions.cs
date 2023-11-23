using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusVRInteractions : MonoBehaviour
{
    public GameObject[] uiObject; // 활성화 할 UI 오브젝트
    public GameObject paper;

    public int arraySize;

    public bool isTouching = false; // 터치 중인지 여부

    // Update is called once per frame
    void Start()
    {

    }

    private void CheckTouchInteraction()
    {
        if(OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            if(isTouching==true)
            {
                if(paper != null)
                {
                    if(paper.name == "Paper_1")
                    {
                        uiObject[0].SetActive(true);
                    }
                    else if(paper.name == "Paper_2")
                    {
                        uiObject[1].SetActive(true);
                    }
                    else if (paper.name == "Paper_3")
                    {
                        uiObject[2].SetActive(true);
                    }
                    else if (paper.name == "Paper_4")
                    {
                        uiObject[3].SetActive(true);
                    }
                    else if (paper.name == "Paper_5")
                    {
                        uiObject[4].SetActive(true);
                    }
                    else if (paper.name == "Paper_6")
                    {
                        uiObject[5].SetActive(true);
                    }
                    else if (paper.name == "Paper_7")
                    {
                        uiObject[6].SetActive(true);
                    }
                }
            }
        }
        else if(isTouching == false)
        {
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
            paper = other.gameObject;
            isTouching = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        isTouching = false;
    }
}
