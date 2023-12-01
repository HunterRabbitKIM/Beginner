using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PWCheck : MonoBehaviour
{
    public GameObject potal;
    private int[] pw = new int[4] { 1, 2, 0, 2 };
    private int[] userpw = new int[4] { 0, 0, 0, 0 };
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setpw(int btnum, int matnum)
    {
        userpw[btnum] = matnum;
    }
    
    public void checkpw()
    {
        if (pw.SequenceEqual(userpw))
        {
            UIManager.instance.ShowplayerLine("지하실로 들어갈 수 있을 것 같다");
            potal.gameObject.SetActive(true);
            potal.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
        //Debug.Log("["+userpw[0]+"," +userpw[1]+","+userpw[2]+","+userpw[3]+"]");
    }
}
