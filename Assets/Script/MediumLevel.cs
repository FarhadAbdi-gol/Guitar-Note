using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumLevel : MonoBehaviour, IStrategy
{
    public static float MediumSpeed;
    public AudioSource audioSource;
    public AudioClip clip;
     List<int> MediumList = new List<int> {2,4,3,1,3,4,3,2,4,1,4,2,3,1,4,2,3,4,2,1,3,4,2,1,3,4,3,2,3,4,1,3,2,4,3,1,4,2,3,1,4,2,3,1,2,4,1,3,2,4,
                                           2,4,3,1,3,4,3,2,4,1,4,2,3,1,4,2,3,4,2,1,3,4,2,1,3,4,3,2,3,4,1,3,2,4,3,1,4,2,3,1,4,2,3,1,2,3,2,1,4,2};

    ControlLevel CL;

    private void Awake()
    {
        CL = GameObject.Find("ControleLevel").gameObject.GetComponent<ControlLevel>();
    }

    public void setListNote()
    {
        StartCoroutine(CL.EasyNote(.7f, MediumList));
    }

    public void setMusic()
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
