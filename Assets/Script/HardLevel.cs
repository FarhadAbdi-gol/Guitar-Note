using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardLevel : MonoBehaviour, IStrategy
{
    public static float HardSpeed;
    public AudioSource audioSource;
    public AudioClip clip;

    List<int> HardList = new List<int> { 1, 2, 4, 3, 2, 1, 3, 4, 2, 3, 2, 4, 1, 4, 1, 4, 2, 3, 3, 4, 2, 3, 1, 4, 2, 3, 4, 2, 2, 1, 1, 4, 3, 4, 2, 3, 2, 3, 1, 4, 3, 1, 2, 4, 3, 1, 2, 2, 3, 1, 3, 2, 3, 1, 3, 4, 4, 2, 1, 2, 4, 1, 1, 2, 4, 1, 3, 1, 3, 2, 2, 3, 4, 1, 2, 2, 1, 2, 4, 3 };
    ControlLevel CL;

    private void Awake()
    {
        CL = GameObject.Find("ControleLevel").gameObject.GetComponent<ControlLevel>();
    }
    public void setLevel(float speed)
    {
        speed = HardSpeed;
    }

    public void setListNote()
    {
        StartCoroutine(CL.EasyNote(.4f, HardList));
    }

    public void setMusic()
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
