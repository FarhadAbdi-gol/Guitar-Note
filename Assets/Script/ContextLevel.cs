using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextLevel : MonoBehaviour
{
    private IStrategy strLevel;
    public GameObject current_background;
    public GameObject background;
    public GameObject dancer;
    public GameObject Note;
    public GameObject Button;

    public AudioSource audioSource;
    public AudioClip clip_EasyLevel;
    public AudioClip clip_MediumLevel;
    public AudioClip clip_HardLevel;

    public GameObject lose_Fire;
    public GameObject win_Particaler;

    public void WinPlayer(GameObject _background)
    {
        current_background.gameObject.SetActive(false);
        _background.gameObject.SetActive(true);
    }
    public void LosePlayer(GameObject _background)
    {
        current_background.gameObject.SetActive(false);
        _background.gameObject.SetActive(true);
    }
    public void OnTriggerEnter(Collider col)
    {
        Destroy(Note);
        Button.GetComponent<Animator>().Play("Press");
    }
    public void OnTriggerExit(Collider col)
    {
        Destroy(Note,1);
        Button.GetComponent<Animator>().Play("Miss");
    }
}
