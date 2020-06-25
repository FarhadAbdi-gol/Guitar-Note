using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EasyLevel : MonoBehaviour, IStrategy 
{
    public static float EasySpeed;
    public AudioSource audioSource;
    public AudioClip clip;
    List<int> EasyList = new List<int> { 3, 1, 2, 3, 4, 2, 3, 1, 2, 4, 3, 4, 2, 1, 2, 4, 3, 1, 2, 1, 3, 4, 2, 3, 1, 2, 3, 4, 1, 2 };

    ControlLevel CL;

    private void Awake()
    {
        CL = GameObject.Find("ControleLevel").gameObject.GetComponent<ControlLevel>();
    }


    public void setListNote()
    {
        StartCoroutine(CL.EasyNote(1f, EasyList));
    }

    public void setMusic()
    {
        if(clip!=null  && audioSource!=null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    
}
