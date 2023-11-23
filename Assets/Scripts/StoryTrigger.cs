using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    public GameObject[] storyUi;
    
    public GameObject storyTrigger;

    [SerializeField]
    private int arraySize;

    public bool isTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        CheckStory();
    }

    private void CheckStory()
    {
        Debug.Log("test1");
        if(isTrigger == true)
        {
            Debug.Log("test2");
            if (storyTrigger != null)
            {
                Debug.Log(storyTrigger.name);
                if (storyTrigger.name == "Story_Trigger1")
                {
                    Debug.Log("test3");
                    storyUi[0].SetActive(true);
                }
                else if (storyTrigger.name == "Story_Trigger2")
                {
                    storyUi[1].SetActive(true);
                }
                else if (storyTrigger.name == "Story_Trigger3")
                {
                    storyUi[2].SetActive(true);
                }
                else if (storyTrigger.name == "Story_Trigger4")
                {
                    storyUi[3].SetActive(true);
                }
                else if (storyTrigger.name == "Story_Trigger5")
                {
                    storyUi[4].SetActive(true);
                }
                else if (storyTrigger.name == "Story_Trigger6")
                {
                    storyUi[5].SetActive(true);
                }
                else if (storyTrigger.name == "Story_Trigger7")
                {
                    storyUi[6].SetActive(true);
                }
                else if (storyTrigger.name == "Story_Trigger8")
                {
                    storyUi[7].SetActive(true);
                }
                else if (storyTrigger.name == "Story_Trigger9")
                {
                    storyUi[8].SetActive(true);
                }
                else if (storyTrigger.name == "Story_Trigger10")
                {
                    storyUi[9].SetActive(true);
                }
                else if (storyTrigger.name == "Story_Trigger11")
                {
                    storyUi[10].SetActive(true);
                }
                else if (storyTrigger.name == "Story_Trigger12")
                {
                    storyUi[11].SetActive(true);
                }
                else if (storyTrigger.name == "Story_Trigger13")
                {
                    storyUi[12].SetActive(true);
                }
            }
        }
        else if(isTrigger == false)
        {
            for(int i = 0; i < arraySize; i++)
            {
                storyUi[i].SetActive(false);
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Story")
        {
            storyTrigger = other.gameObject;
            isTrigger = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        isTrigger = false;
    }

}
