using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour, IHandle
{
    private readonly string ItemName = "ladder";
    
    public string myName()
    {
        return ItemName;
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}
