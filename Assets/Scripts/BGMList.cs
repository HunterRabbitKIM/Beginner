using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMList : MonoBehaviour
{
    [System.Serializable]
    public struct BgmType
    {
        public string name;
        public AudioClip audio;
    }

    public BgmType[] TheBGMList;

    private AudioSource BGM;
    private string NowBGMname = "";

    void Start()
    {
        BGM = gameObject.AddComponent<AudioSource>();
        BGM.loop = true;
        if (TheBGMList.Length > 0) PlayBGM(TheBGMList[0].name);
    }

    public void PlayBGM(string name)
    {
        if (NowBGMname.Equals(name)) return;

        for (int i = 0; i < TheBGMList.Length; ++i)
            if (TheBGMList[i].name.Equals(name))
            {
                BGM.clip = TheBGMList[i].audio;
                BGM.Play();
                NowBGMname = name;
            }
    }

    void Update()
    {
        
    }

    
}
