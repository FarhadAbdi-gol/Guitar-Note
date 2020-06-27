
using System.Collections.Generic;
using UnityEngine;

public class MediumLevel : MonoBehaviour, IStrategy
{
    public bool CheckClipM;
    List<int> MediumList = new List<int> {2,4,3,1,3,4,3,2,4,1,4,2,3,1,4,2,3,4,2,1,3,4,2,1,3,4,3,2,3,4,1,3,2,4,3,1,4,2,3,1,4,2,3,1,2,4,1,3,2,4,
                                           2,4,3,1,3,4,3,2,4,1,4,2,3,1,4,2,3,4,2,1,3,4,2,1,3,4,3,2,3,4,1,3,2,4,3,1,4,2,3,1,4,2,3,1,2,3,2,1,4,2};

    ControlLevel CL;

    private void Awake()
    {
        CL = GameObject.Find("ControleLevel").gameObject.GetComponent<ControlLevel>();
    }

    public void setListNote()
    {
        StartCoroutine(CL.CreateNote(.7f, MediumList));
    }

    public void setMusic()
    {
        if (CL.clip_MediumLevel != null && CL.audioSource != null && CheckClipM == false)
        {
            CL.audioSource.PlayOneShot(CL.clip_MediumLevel);
            CheckClipM = true;
        }
    }
    public void StopMusic()
    {
        if (CL.clip_EasyLevel != null && CL.audioSource != null && CheckClipM == true)
        {
            CL.audioSource.Stop();
            CheckClipM = false;
        }
    }
    public void MuteMusic()
    {
        if (CL.clip_EasyLevel != null && CL.audioSource != null && CheckClipM == true)
        {
                CL.audioSource.mute = !CL.audioSource.mute;
        }
    }
}
