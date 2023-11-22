using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    public GameObject[] storyUi;
    public LayerMask layerMask;
    public GameObject storyTrigger;

    [SerializeField]
    private int arraySize;

    private bool isTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        arraySize = 13;
        storyUi = new GameObject[arraySize];
    }
    private void Update()
    {
        CheckStory();
    }

    private void CheckStory()
    {
        if(!isTrigger)
        {
            if(storyTrigger != null)
            {
                if (storyTrigger.name == "StoryTrigger1")
                {
                    isTrigger = true;
                    storyUi[0].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger2")
                {
                    isTrigger = true;
                    storyUi[1].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger3")
                {
                    isTrigger = true;
                    storyUi[2].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger4")
                {
                    isTrigger = true;
                    storyUi[3].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger5")
                {
                    isTrigger = true;
                    storyUi[4].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger6")
                {
                    isTrigger = true;
                    storyUi[5].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger7")
                {
                    isTrigger = true;
                    storyUi[6].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger8")
                {
                    isTrigger = true;
                    storyUi[7].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger9")
                {
                    isTrigger = true;
                    storyUi[8].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger10")
                {
                    isTrigger = true;
                    storyUi[9].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger11")
                {
                    isTrigger = true;
                    storyUi[10].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger12")
                {
                    isTrigger = true;
                    storyUi[11].SetActive(true);
                }
                else if (storyTrigger.name == "StoryTrigger13")
                {
                    isTrigger = true;
                    storyUi[12].SetActive(true);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Story"))
        {
            for(int i = 0; i < arraySize; i++)
            {
                storyTrigger = other.gameObject;
            }
        }
    }

}
