using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour,IHandle
{
    private readonly string ItemName = "key";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string myName()
    {
        return ItemName;
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
