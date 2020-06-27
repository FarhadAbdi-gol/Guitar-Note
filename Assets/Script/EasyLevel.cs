
using System.Collections.Generic;
using UnityEngine;

public class EasyLevel : MonoBehaviour, IStrategy 
{
    public bool mute;
    public bool CheckClipE;
    List<int> EasyList = new List<int> { 3, 1, 2, 3, 4, 2, 3, 1, 2, 4, 3, 4, 2, 1, 2, 4, 3, 1, 2, 1, 3, 4, 2, 3, 1, 2, 3, 4, 1, 2, 2, 4, 3, 4, 2, 1, 2, 4, 3, 1, 3, 4, 2, 3, 1, 2, 3, 4, 1, 2,
                                         3, 1, 2, 3, 4, 2, 3, 1, 2, 4, 3, 4, 2, 1, 2, 4, 3, 1, 2, 1, 3, 4, 2, 3, 1, 2, 3, 4, 1, 2, 3, 1, 2, 3, 4, 2, 3, 1, 2, 4, 3, 4, 2, 1, 2, 4, 3, 1, 2, 1};

    ControlLevel CL;

    private void Awake()
    {
        CL = GameObject.Find("ControleLevel").gameObject.GetComponent<ControlLevel>();
    }


    public void setListNote()
    {
        StartCoroutine(CL.CreateNote(1f, EasyList));
    }

    public void setMusic()
    {
        if (CL.clip_EasyLevel != null && CL.audioSource != null && CheckClipE == false)
        {
            CL.audioSource.PlayOneShot(CL.clip_EasyLevel);
            CheckClipE = true;
        }
    }

    public void StopMusic()
    {
        if(CL.clip_EasyLevel != null && CL.audioSource != null && CheckClipE == true)
        {
            CL.audioSource.Stop();
            CheckClipE = false;
        }
    }
    public void MuteMusic()
    {
        if (CL.clip_EasyLevel != null && CL.audioSource != null && CheckClipE == true)
        {
                CL.audioSource.mute = !CL.audioSource.mute;
        }
    }
}
