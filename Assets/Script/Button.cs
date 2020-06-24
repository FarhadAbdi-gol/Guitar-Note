using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    [SerializeField]
    private LayerMask clickablesLayer;
    private GameObject note_prefab;
    private ControlLevel CL;

    void Awake()
    {
        CL =GameObject.Find("ControleLevel").gameObject.GetComponent<ControlLevel>();
    }

    void Update()
    {
            if(Note.note_Stay)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickablesLayer))
                    {
                        if (hit.transform.name.Contains("Button"))
                        {
                            GiveNote();
                        }
                    }
                }
            }
       

        //if(note_prefab==null)
        //{
        //    Note.note_Stay = false;
        //    Note.note_Enter = false;
        //    Note.note_Exit = false;
        //}
    }

    public void GiveNote()
    {
        if(Note.note_Enter)
        {
            Destroy(note_prefab);
            ControlLevel.Score++;
            CL.ShowScore();
            Note.note_Stay = false;
        }
    }
    private void OnTriggerEnter(Collider note)   
    {
      note_prefab = note.gameObject;
    }

}
