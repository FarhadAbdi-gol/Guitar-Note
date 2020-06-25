using System;
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
    #region public variables
    public GameObject Guitar;
    public GameObject dancer;
    public GameObject Level_Panel, Game_Panel, Scores_Panel;//, Repeat_Panel;
  
    public GameObject win_Particaler;
    public GameObject ButtonR, ButtonB, ButtonG, ButtonP;

    public GameObject IndexInstance;
    public GameObject note;
    public GameObject winAnim,loseAnim;
    public GameObject LuckM, LockH;
    public Button MediumBtn , HardBtn; 

    public float noteX;
    public bool course=false;
    public int Indexlist = 0;
    public int Lenghlist;
    public int Conter;
    public Text Scoretxt;
    public Text HighScoretxt;
    public Text ScoretxtRepeat;
    public Text Messagewin;
    public int Score=0;
    public int HighScore=0;
    public int countNote;

    public const string PpsScoreR = "PpsScoreR";
    public const string PpsScoreB = "PpsScoreB";
    public const string PpsScoreG = "PpsScoreG";
    public const string PpsScoreP = "PpsScoreP";
    public const string PpsHighScore = "PpsHighScore";

    //public AudioSource audioSource;
    //public AudioClip clip_EasyLevel;
    //public AudioClip clip_MediumLevel;
    //public AudioClip clip_HardLevel;
    #endregion 

    #region private variables
    EasyLevel EL;
    MediumLevel ML;
    HardLevel HL;
    ButtonCL BtnR,BtnB,BtnG,BtnP;
    Level levelCurrent;
    #endregion 

    #region private Functions
    private void Awake()
    {
        EL = gameObject.AddComponent<EasyLevel>();
        ML = gameObject.AddComponent<MediumLevel>();
        HL = gameObject.AddComponent<HardLevel>();
        BtnR= GameObject.Find("ButtonR").gameObject.GetComponent<ButtonCL>();
        BtnB = GameObject.Find("ButtonB").gameObject.GetComponent<ButtonCL>();
        BtnG = GameObject.Find("ButtonG").gameObject.GetComponent<ButtonCL>();
        BtnP = GameObject.Find("ButtonP").gameObject.GetComponent<ButtonCL>();

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
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call<bool>("moveTaskToBack", true);
            }
            else
            {
                Application.Quit();
            }
        }
        if (Conter > 0)
        {
            Guitar.gameObject.GetComponent<Animator>().enabled = true;
            Guitar.gameObject.GetComponent<Animator>().Play("Guitar");
        }
        else
        {
            Guitar.gameObject.GetComponent<Animator>().enabled = false;         
        }

        CountNote();

        if (countNote==0 && ButtonCL.CheckNote)
        {
            course = true;
            PlayerPrefs.SetInt(PpsScoreB, BtnB.ScoreB);
            PlayerPrefs.SetInt(PpsScoreG, BtnG.ScoreG);
            PlayerPrefs.SetInt(PpsScoreR, BtnR.ScoreR);
            PlayerPrefs.SetInt(PpsScoreP, BtnP.ScoreP);
            PlayerPrefs.Save();

            if (levelCurrent == Level.Easy && Score > 29)
            {
                Scores_Panel.gameObject.SetActive(false);
                Level_Panel.gameObject.SetActive(true);
                ShowScore();
                MediumBtn.interactable = true;
                LuckM.gameObject.SetActive(false);
            }
            else
            {
                Scores_Panel.gameObject.SetActive(false);
                Level_Panel.gameObject.SetActive(true);
                ShowScore();
                MediumBtn.interactable = false;
                Messagewin.text = "Repeat Game";
                Messagewin.gameObject.GetComponentInChildren<Text>().color = Color.red;
            }
            if (levelCurrent == Level.Medium && Score > 54)
            {
                Scores_Panel.gameObject.SetActive(false);
                Level_Panel.gameObject.SetActive(true);
                ShowScore();
                HardBtn.interactable = true;
                LockH.gameObject.SetActive(false);
            }
            else
            {
                Scores_Panel.gameObject.SetActive(false);
                Level_Panel.gameObject.SetActive(true);
                ShowScore();
                HardBtn.interactable = false;
                Messagewin.text = "Repeat Game";
                Messagewin.gameObject.GetComponentInChildren<Text>().color = Color.red;
            }
            if (levelCurrent == Level.Hard && Score > 79)
            {
                Scores_Panel.gameObject.SetActive(false);
                Level_Panel.gameObject.SetActive(true);
                ShowScore();
                Messagewin.text = "You Win";
                Messagewin.gameObject.GetComponentInChildren<Text>().color = Color.green;
            }
            else
            {
                Scores_Panel.gameObject.SetActive(false);
                Level_Panel.gameObject.SetActive(true);
                ShowScore();
                Messagewin.text = "Repeat Game";
                Messagewin.gameObject.GetComponentInChildren<Text>().color = Color.red;
            }
        }
    }
    private void CountNote()
    {
        GameObject[] NoteCount = GameObject.FindGameObjectsWithTag("note");
        countNote = NoteCount.Length;
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
    #endregion

    #region public Functions
    public void EasyLevel_Btn()
    {
        Level_Panel.gameObject.SetActive(false);
        Scores_Panel.gameObject.SetActive(true);
        levelCurrent =Level.Easy;
        course = true;
    }
    public void MediumLevel_Btn()
    {
        Level_Panel.gameObject.SetActive(false);
        Scores_Panel.gameObject.SetActive(true);
        levelCurrent = Level.Medium;
        course = true;
    }
    public void HardLevel_Btn()
    {
        Level_Panel.gameObject.SetActive(false);
        Scores_Panel.gameObject.SetActive(true);
        levelCurrent = Level.Hard;
        course = true;
    }

    public void ChangedLevel(Level level)
    {
        switch (level)
        { case Level.Easy:
              EL.setListNote();
          break;
          case Level.Medium:
              ML.setListNote();
          break;
          case Level.Hard:
              HL.setListNote();
          break;            
        }
        course = false;
    }

    public IEnumerator EasyNote(float sec, List<int> noteList)
    {
        Lenghlist = noteList.Count;
        if(Indexlist==0)
            Conter = Lenghlist;

        if (Indexlist < noteList.Count)
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
            Conter -=1;
        }
    }

    public void ShowScore()
    {
        Score = PlayerPrefs.GetInt(PpsScoreB)+ PlayerPrefs.GetInt(PpsScoreR)+ PlayerPrefs.GetInt(PpsScoreG) + PlayerPrefs.GetInt(PpsScoreP);
      
            if (Score > HighScore)
            {
                Scoretxt.text = (Score).ToString();
                HighScoretxt.text = (Score).ToString();
                PlayerPrefs.SetString(PpsHighScore, HighScoretxt.text);
                PlayerPrefs.Save();
            }
            else
            {
                Scoretxt.text = (Score).ToString();
                HighScoretxt.text = PlayerPrefs.GetString(PpsHighScore).ToString();
            }      
    }
    public static int IntParseFast(string value)
    {
        int result = 0;
        for (int i = 0; i < value.Length; i++)
        {
            char letter = value[i];
            result = 10 * result + (letter - 48);
        }
        return result;
    }
    public void StartGame()
    {
        Game_Panel.gameObject.SetActive(false);
        Level_Panel.gameObject.SetActive(true);
    }
    public void FAgBtn()
    {
        Application.OpenURL("https://github.com/FarhadAbdi-gol");
    }
    public void Exit()
    {
        Application.Quit();
    }

    ////public void BackofReapet()
    ////{
    ////    Repeat_Panel.gameObject.SetActive(false);
    ////    Level_Panel.gameObject.SetActive(true);
    ////}
    public void BackofLevel()
    {
        Level_Panel.gameObject.SetActive(false);
        Game_Panel.gameObject.SetActive(true);
    }
    //public void Repeat()
    //{
    //    course = true;

    //    if (levelCurrent == Level.Easy)
    //        EasyLevel_Btn();
    //    if (levelCurrent == Level.Medium)
    //        MediumLevel_Btn();
    //    if (levelCurrent == Level.Hard)
    //        HardLevel_Btn();
    //}
    #endregion

}

