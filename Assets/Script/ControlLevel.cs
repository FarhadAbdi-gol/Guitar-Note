using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Level
{
    Easy, Medium, Hard
}

public class ControlLevel : MonoBehaviour
{
    
    public GameObject current_background;
    public GameObject background;
    public GameObject dancer;
    public GameObject Note;
    public GameObject Button;
    public GameObject Level_Panel;
    public GameObject Game_Panel;

    public int Indexlist=0;
    public GameObject ButtonR;
    public GameObject ButtonB;
    public GameObject ButtonG;
    public GameObject ButtonP;
    //public GameObject StringR;
    //public GameObject StringB;
    //public GameObject StringG;
    //public GameObject StringP;
    public GameObject IndexInstance;
    public GameObject note;
    public float noteX;
    public bool course=false;
    
    public Text Scoretxt;
    public static int Score;
    //public AudioSource audioSource;
    //public AudioClip clip_EasyLevel;
    //public AudioClip clip_MediumLevel;
    //public AudioClip clip_HardLevel;

    public GameObject lose_Fire;
    public GameObject win_Particaler;
    //Color colorNote;
    EasyLevel EL;
    MediumLevel ML;
    HardLevel HL;
    Level levelCurrent;

    private void Awake()
    {
        EL = gameObject.AddComponent<EasyLevel>();
        ML = gameObject.AddComponent<MediumLevel>();
        HL = gameObject.AddComponent<HardLevel>();
    }

 

    private void Update()
    {
        if (course)
        {
            if (levelCurrent == Level.Easy)
                ChangedLevel(Level.Easy);
            if (levelCurrent == Level.Medium)
                ChangedLevel(Level.Medium);
            if (levelCurrent == Level.Hard)
                ChangedLevel(Level.Hard);
        }
    }

    public void EasyLevel_Btn()
    {
       Level_Panel.gameObject.SetActive(false);
        Game_Panel.gameObject.SetActive(true);
        levelCurrent =Level.Easy;
        course = true;
    }
    public void MediumLevel_Btn()
    {
        Level_Panel.gameObject.SetActive(false);
        Game_Panel.gameObject.SetActive(true);

        levelCurrent = Level.Medium;
        course = true;
    }
    public void HardLevel_Btn()
    {
        Level_Panel.gameObject.SetActive(false);
        Game_Panel.gameObject.SetActive(true);

        levelCurrent = Level.Hard;
        course = true;
    }

    public void ChangedLevel(Level level)
    {
        switch (level)
        {
             case Level.Easy:
                {
                    EL.setListNote();
                }
                 break;
            case Level.Medium:
                {
                    ML.setListNote();
                }
                break;
            case Level.Hard:
                {
                    HL.setListNote();
                }
                break;
                
        }
        course = false;
    }

    public IEnumerator EasyNote(float sec, List<int> noteList)
    {   
        if(Indexlist < noteList.Count)
        {
            yield return new WaitForSeconds(sec);
            if (noteList[Indexlist] == 1)
            {
                GameObject NoteR = Instantiate(note, new Vector3(ButtonR.transform.position.x, IndexInstance.transform.position.y + 2f, IndexInstance.transform.position.z), Quaternion.identity) as GameObject;
                NoteR.GetComponent<MeshRenderer>().material.color = ButtonR.gameObject.GetComponent<MeshRenderer>().material.color;
            }
            else if (noteList[Indexlist] == 2)
            {
                GameObject NoteB = Instantiate(note, new Vector3(ButtonB.transform.position.x, IndexInstance.transform.position.y + 2f, IndexInstance.transform.position.z), Quaternion.identity) as GameObject;
                NoteB.GetComponent<MeshRenderer>().material.color = ButtonB.gameObject.GetComponent<MeshRenderer>().material.color;
            }
            else if (noteList[Indexlist] == 3)
            {
                GameObject NoteG = Instantiate(note, new Vector3(ButtonG.transform.position.x, IndexInstance.transform.position.y + 2f, IndexInstance.transform.position.z), Quaternion.identity) as GameObject;
                NoteG.GetComponent<MeshRenderer>().material.color = ButtonG.gameObject.GetComponent<MeshRenderer>().material.color;

            }
            else if (noteList[Indexlist] == 4)
            {
                GameObject NoteP = Instantiate(note, new Vector3(ButtonP.transform.position.x, IndexInstance.transform.position.y + 2f, IndexInstance.transform.position.z), Quaternion.identity) as GameObject;
                NoteP.GetComponent<MeshRenderer>().material.color = ButtonP.gameObject.GetComponent<MeshRenderer>().material.color;
            }
            course = true;
            Indexlist += 1;
        }     
    }
    public void ShowScore()
    {
        Scoretxt.text = (Score/4).ToString();
    }
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

