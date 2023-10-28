using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager m_instance;
    public TextMeshProUGUI PlayerLine;

    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
    }

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowplayerLine(string line)
    {
        PlayerLine.text = line;
        StartCoroutine(Showingline());
    }

    IEnumerator Showingline()
    {
        PlayerLine.enabled = true;
        yield return new WaitForSeconds(5f);
        PlayerLine.enabled = false;
    }
}
