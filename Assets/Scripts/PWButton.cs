using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PWButton : MonoBehaviour
{
    
    public GameObject button;
    AudioSource sound;
    private bool isPressed;
    public Material[] mat = new Material[3];
    int matnum;
    public GameObject pwcheck;

    public int buttonnum;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
        matnum = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0, -0.003f);
            sound.Play();
            isPressed = true;
            ChangeCubeMat();
            pwcheck.GetComponent<PWCheck>().setpw(buttonnum,matnum);
            pwcheck.GetComponent<PWCheck>().checkpw();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        button.transform.localPosition = new Vector3(0, 0, -0.05f);
        isPressed = false;
    }

    public void ChangeCubeMat()
    {
        matnum = ++matnum % 3;

        // Change Material
        button.GetComponent<MeshRenderer>().material = mat[matnum];
    }
}