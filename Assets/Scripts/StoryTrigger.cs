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

    private void CheckStory()
    {
        if(isTrigger == true)
        {
            if(storyTrigger != null)
            {
                if (storyTrigger.name == "StoryTrigger1")
                {
                    Debug.Log(storyTrigger.name);
                    storyUi[0].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger2")
                {
                    storyUi[1].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger3")
                {
                    storyUi[2].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger4")
                {
                    storyUi[3].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger5")
                {
                    storyUi[4].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger6")
                {
                    storyUi[5].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger7")
                {
                    storyUi[6].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger8")
                {
                    storyUi[7].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger9")
                {
                    storyUi[8].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger10")
                {
                    storyUi[9].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger11")
                {
                    storyUi[10].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger12")
                {
                    storyUi[11].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger13")
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
        Debug.Log(other.tag);

        if(other.tag == "Story")
        {
            
            storyTrigger = other.gameObject;
            isTrigger = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == null)
        {
            isTrigger = false;
        }
    }

}
