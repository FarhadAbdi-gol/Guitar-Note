using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Level
{
    Easy, Medium, Hard, ui
}

public class ControlLevel : MonoBehaviour
{
    #region public variables
    public GameObject Guitar;
    public GameObject dancer;
    public GameObject Level_Panel, Game_Panel, Scores_Panel;
  
    public GameObject win_Particaler;
    public GameObject ButtonR, ButtonB, ButtonG, ButtonP;

    public GameObject IndexInstance;
    public GameObject note;
    public GameObject winAnim,loseAnim;
    public GameObject LockM, LockH;
    public Button EasyBtn, MediumBtn , HardBtn, ResetGameBtn; 

    public float noteX;
    public bool course=false;
    public int Indexlist = 0;
    public int Lenghlist;
    public int Conter;
    public Text Scoretxt;
    public Text HighScoretxt;
    public Text Messagewin;
    public int Score=0;
    public int HighScore=0;
    public int countNote;
    public string currentLevel;

    public const string PpsHighScore = "PpsHighScore";
    public const string PpsScore = "PpsScore";

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

    private void Start()
    {
       // PlayerPrefs.DeleteAll();
        levelCurrent = Level.ui;
        currentLevel = levelCurrent.ToString();
        BtnB.txtB.text = "";
        BtnR.txtR.text = "";
        BtnG.txtG.text = "";
        BtnP.txtP.text = "";
        if (PlayerPrefs.HasKey(PpsScore))
            Scoretxt.text = PlayerPrefs.GetInt(PpsScore).ToString();
        if(PlayerPrefs.HasKey(PpsHighScore))
            HighScoretxt.text = PlayerPrefs.GetInt(PpsHighScore).ToString();
        if (PlayerPrefs.GetInt(PpsHighScore) > 0)
            ResetGameBtn.gameObject.SetActive(true);
    }
    private void Update()
    {   
        if(PlayerPrefs.GetInt(PpsHighScore)==300)
        {
            EasyBtn.interactable = false;
            MediumBtn.interactable = false;
            HardBtn.interactable = false;
        }
        if(PlayerPrefs.GetInt(PpsHighScore) == 0)
        {
            ResetGameBtn.gameObject.SetActive(false);
        }
        if(PlayerPrefs.GetInt(PpsHighScore) >= 200)
        {
            LockH.gameObject.SetActive(true);
        }
        if(PlayerPrefs.GetInt(PpsHighScore) >= 100)
        {
            LockM.gameObject.SetActive(true);
        }
        if (course == true && levelCurrent != Level.ui)
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
        if (Conter > 0 && levelCurrent != Level.ui)
        {
            Guitar.gameObject.GetComponent<Animator>().enabled = true;
            Guitar.gameObject.GetComponent<Animator>().Play("Guitar");
        }
        else
        {
            Guitar.gameObject.GetComponent<Animator>().enabled = false;         
        }

        CountNote();

        if (countNote==0 && ButtonCL.CheckNote==true && levelCurrent != Level.ui)
        {
            course = false;
           // Scores_Panel.gameObject.SetActive(false);
            Level_Panel.gameObject.SetActive(true);
            ShowScore();
            if (PlayerPrefs.GetInt(PpsHighScore) > 0)
                ResetGameBtn.gameObject.SetActive(true);

            if (levelCurrent == Level.Easy && Score > 99)
            {              
                MediumBtn.interactable = true;
                LockM.gameObject.SetActive(false);
                EasyBtn.interactable = false;
                Messagewin.text = "";
            }
            else
            {
                Messagewin.text = "Repeat Game";
                Messagewin.gameObject.GetComponentInChildren<Text>().color = Color.red;
            }

            if (levelCurrent == Level.Medium && Score > 99)
            {
                HardBtn.interactable = true;
                LockH.gameObject.SetActive(false);
                MediumBtn.interactable = false;
                Messagewin.text = "";
            }
            else
            {
                Messagewin.text = "Repeat Game";
                Messagewin.gameObject.GetComponentInChildren<Text>().color = Color.red;
            }
            if (levelCurrent == Level.Hard && Score > 99)
            {
                Messagewin.text = "You Win";
                Messagewin.gameObject.GetComponentInChildren<Text>().color = Color.green;
            }
            else
            {
                Messagewin.text = "Repeat Game";
                Messagewin.gameObject.GetComponentInChildren<Text>().color = Color.red;
            }
            levelCurrent = Level.ui;
        }
        currentLevel = levelCurrent.ToString();
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
        if(levelCurrent != Level.Easy && levelCurrent == Level.ui)
        {
            levelCurrent = Level.Easy;
            Levels_btn();
        }
      
    }
    public void MediumLevel_Btn()
    {
        if (levelCurrent != Level.Medium && levelCurrent == Level.ui)
        {
            levelCurrent = Level.Medium;
            Levels_btn();
        }
    }
    public void HardLevel_Btn()
    {
        if(levelCurrent != Level.Medium && levelCurrent == Level.ui)
        {
            levelCurrent = Level.Hard;
            Levels_btn();
        }
    }

    public void Levels_btn()
    {
        ButtonCL.CheckNote = false;
        Level_Panel.gameObject.SetActive(false);
        Scores_Panel.gameObject.SetActive(true);
        course = true;
        BtnB.txtB.text = "";
        BtnG.txtG.text = "";
        BtnR.txtR.text = "";
        BtnP.txtP.text = "";
        Indexlist = 0;
        Score = 0;
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
            
            Indexlist += 1;
            Conter -=1;
           
            course = true;
        }
    }

    public void ShowScore()
    {
        Score = BtnR.ScoreR + BtnB.ScoreB + BtnG.ScoreG + BtnP.ScoreP;
        PlayerPrefs.SetInt(PpsScore, Score);
        PlayerPrefs.Save();
        Scoretxt.text = (Score).ToString();

        if(PlayerPrefs.HasKey(PpsHighScore))
        {
            HighScore = PlayerPrefs.GetInt(PpsHighScore);
            if (Score == Lenghlist)
            {
                HighScore += Score;
                PlayerPrefs.SetInt(PpsHighScore, HighScore);
                PlayerPrefs.Save();
                HighScoretxt.text = PlayerPrefs.GetInt(PpsHighScore).ToString();
                Delletkey();
            }
            else
            {
                if(HighScore > Score)
                {
                    PlayerPrefs.SetInt(PpsHighScore, HighScore);
                    PlayerPrefs.Save();
                    HighScoretxt.text = PlayerPrefs.GetInt(PpsHighScore).ToString();
                    Delletkey();
                }
                else if(HighScore < Score)
                {
                    HighScore = Score;
                    PlayerPrefs.SetInt(PpsHighScore, HighScore);
                    PlayerPrefs.Save();
                    HighScoretxt.text = PlayerPrefs.GetInt(PpsHighScore).ToString();
                    Delletkey();
                }
            }                      
        }
        else
        {
            HighScore = Score;
            PlayerPrefs.SetInt(PpsHighScore, HighScore);
            PlayerPrefs.Save();
            HighScoretxt.text = PlayerPrefs.GetInt(PpsHighScore).ToString();
            Delletkey();
        }
    }

    public void Delletkey()
    {
        BtnR.ScoreR = 0;
        BtnB.ScoreB = 0;
        BtnG.ScoreG = 0;
        BtnP.ScoreP = 0;
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
    public void Reset_Game()
    {
        PlayerPrefs.SetInt(PpsHighScore, 0);
        PlayerPrefs.SetInt(PpsScore, 0);
        PlayerPrefs.Save();
        Scoretxt.text= PlayerPrefs.GetInt(PpsScore).ToString();
        HighScoretxt.text = PlayerPrefs.GetInt(PpsHighScore).ToString();
        EasyBtn.interactable = true;
        MediumBtn.interactable = false;
        HardBtn.interactable = false;
        ResetGameBtn.interactable = false;
        ResetGameBtn.gameObject.SetActive(false);
        Scores_Panel.gameObject.SetActive(false);
    }

    public void BackofLevel()
    {
        if (levelCurrent == Level.ui)
        {
            Scores_Panel.gameObject.SetActive(false);
            Level_Panel.gameObject.SetActive(false);
            Game_Panel.gameObject.SetActive(true);
        }
        else
        {
            levelCurrent = Level.ui;
        }
    }
    #endregion

}

