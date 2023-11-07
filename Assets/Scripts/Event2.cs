using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event2 : MonoBehaviour
{
    public GameObject ladder;
    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other)
    {
        IHandle item = other.GetComponent<IHandle>();
        if (item!=null&&!item.myName().Equals(""))
        {
            if (item.myName().Equals("ladder"))
            {
                ladder.gameObject.SetActive(true);
                item.destroy();
            }
        }
    }
}
