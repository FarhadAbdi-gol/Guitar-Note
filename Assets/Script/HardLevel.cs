
using System.Collections.Generic;
using UnityEngine;

public class HardLevel : MonoBehaviour, IStrategy
{
    public bool CheckClipH;
    List<int> HardList = new List<int> { 1, 2, 4, 3, 2, 1, 3, 4, 2, 3, 2, 4, 1, 3, 2, 4, 1, 3, 2, 4, 1, 3, 2, 4, 1, 3, 4, 2, 1, 3, 4, 1, 2, 3, 4, 1, 3, 2, 1, 4, 3, 1, 2, 4, 3, 1, 2, 4, 3, 1,
                                         4, 2, 3, 1, 4, 2, 3, 1, 2, 3, 4, 2, 4, 1, 3, 2, 4, 1, 3, 2, 4, 1, 2, 3, 4, 1, 3, 2, 4, 1, 3, 2, 4, 1, 2, 3, 4, 2, 1, 3, 4, 1, 2, 3, 4, 1, 4, 1, 3, 2};
    ControlLevel CL;

    private void Awake()
    {
        CL = GameObject.Find("ControleLevel").gameObject.GetComponent<ControlLevel>();
    }
    void Update()
    {

    }
    public void setListNote()
    {
        StartCoroutine(CL.EasyNote(.4f, HardList));
    }

    public void setMusic()
    {
        if (CL.clip_HardLevel != null && CL.audioSource != null && CheckClipH==false)
        {
            CL.audioSource.PlayOneShot(CL.clip_HardLevel);
            CheckClipH = true;
        }
    }
    public void StopMusic()
    {
        if (CL.clip_EasyLevel != null && CL.audioSource != null && CheckClipH == true)
        {
            CL.audioSource.Stop();
            CheckClipH = false;
        }
    }
    public void MuteMusic()
    {
        if (CL.clip_EasyLevel != null && CL.audioSource != null && CheckClipH == true)
        {
                CL.audioSource.mute = !CL.audioSource.mute;
        }
    }
}
