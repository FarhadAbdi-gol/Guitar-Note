using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public static bool note_Enter;
    public static bool note_Stay;
    public static bool note_Exit;

    //void Start()
    //{
    
    //}

    //void Update()
    //{

    //}

    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.name.Contains( "Button"))
        {
            note_Enter = true;
            //Debug.Log("Enter");
        }
    }
    private void OnTriggerStay(Collider target)
    {
        if (target.gameObject.name.Contains( "Button"))
        {
            note_Stay = true;
           // Debug.Log("Stay");
        }
    }
    private void OnTriggerExit(Collider target)
    {
        if (target.gameObject.name.Contains("Button"))
        {
            note_Exit=true;           
            Destroy(gameObject,1f);
            Debug.Log("Destroy by Exit");
            note_Enter = false;
            note_Stay = false;
            note_Exit = false;
        }
    }
}
