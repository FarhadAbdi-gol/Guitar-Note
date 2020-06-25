
using UnityEngine;
using UnityEngine.UI;

public enum ButtonType
{
    ButtonR, ButtonB, ButtonG, ButtonP
}
public class ButtonCL : MonoBehaviour
{
    #region public variables
    public bool note_StayR, note_StayB, note_StayG, note_StayP;
    public int ScoreR, ScoreB, ScoreG, ScoreP;
    public Text txtR, txtB, txtG, txtP;
    public ButtonType current_Button;
    public static bool CheckNote;
    #endregion

    #region private variables
    GameObject note_prefab;
    ControlLevel CL;
    
    #endregion


    #region private Functions
    void Awake()
    {
        CL = GameObject.Find("ControleLevel").gameObject.GetComponent<ControlLevel>();
    }

    void Update()
    {
        if(!CL.Level_Panel.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.transform.name == "ButtonR")
                    {
                        CL.ButtonR.GetComponent<Animator>().Play("ButtonR");
                        if (note_StayR == true)
                            GiveNote();
                    }
                    else if (hit.transform.name == "ButtonB")
                    {
                        CL.ButtonB.GetComponent<Animator>().Play("ButtonB");
                        if (note_StayB == true)
                            GiveNote();
                    }

                    else if (hit.transform.name == "ButtonG")
                    {
                        CL.ButtonG.GetComponent<Animator>().Play("ButtonG");
                        if (note_StayG == true)
                            GiveNote();
                    }
                    else if (hit.transform.name == "ButtonP")
                    {
                        CL.ButtonP.GetComponent<Animator>().Play("ButtonP");
                        if (note_StayP == true)
                            GiveNote();
                    }
                }
            }
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit))
                {
                    if (hit.transform.name == "ButtonR")
                    {
                        CL.ButtonR.GetComponent<Animator>().Play("ButtonR");
                        if (note_StayR == true)
                            GiveNote();
                    }
                    else if (hit.transform.name == "ButtonB")
                    {
                        CL.ButtonB.GetComponent<Animator>().Play("ButtonB");
                        if (note_StayB == true)
                            GiveNote();
                    }

                    else if (hit.transform.name == "ButtonG")
                    {
                        CL.ButtonG.GetComponent<Animator>().Play("ButtonG");
                        if (note_StayG == true)
                            GiveNote();
                    }
                    else if (hit.transform.name == "ButtonP")
                    {
                        CL.ButtonP.GetComponent<Animator>().Play("ButtonP");
                        if (note_StayP == true)
                            GiveNote();
                    }
                }
            }
        }
       
    }

    private void GiveNote()
    {
        CheckNote = true;
        if (current_Button == ButtonType.ButtonR)
         {
            CL.winAnim.gameObject.GetComponent<Animator>().Play("NoteR");
            ScoreR++;
            txtR.text = ScoreR.ToString();
            note_StayR = false;
            Destroy(note_prefab);
         }
         else if (current_Button == ButtonType.ButtonB)
         {
            CL.winAnim.gameObject.GetComponent<Animator>().Play("NoteB");
            ScoreB++;
            txtB.text = ScoreB.ToString();
            note_StayB = false;
            Destroy(note_prefab);
         }
         else if (current_Button == ButtonType.ButtonG)
         {
            CL.winAnim.gameObject.GetComponent<Animator>().Play("NoteG");
            ScoreG++;
            txtG.text = ScoreG.ToString();
            note_StayG = false;
            Destroy(note_prefab);
         }
         else if (current_Button == ButtonType.ButtonP)
         {
            CL.winAnim.gameObject.GetComponent<Animator>().Play("NoteP");
            ScoreP++;
            txtP.text = ScoreP.ToString();
            note_StayP = false;
            Destroy(note_prefab);
         }
    }

    private void OnTriggerEnter(Collider target)
    {
        note_prefab = target.gameObject;
        if (target.gameObject.name.Contains("Note"))
        {
            note_prefab = target.gameObject;
            if (current_Button == ButtonType.ButtonR)
                note_StayR = true;
            else if (current_Button == ButtonType.ButtonB)
                note_StayB = true;
            else if (current_Button == ButtonType.ButtonG)
                note_StayG = true;
            else if (current_Button == ButtonType.ButtonP)
                note_StayP = true;
        }
    }

    private void OnTriggerStay(Collider target)
    {
        note_prefab = target.gameObject;
    }

    private void OnTriggerExit(Collider target)
    {
        if (target.gameObject.name.Contains("Note"))
        {
            Destroy(target.gameObject, 1f);
            CL.loseAnim.gameObject.SetActive(true);
            CL.loseAnim.gameObject.GetComponent<Animator>().Play("Loss");

            if (current_Button == ButtonType.ButtonR)
                note_StayR = false;
            else if (current_Button == ButtonType.ButtonB)
                note_StayB = false;
            else if (current_Button == ButtonType.ButtonG)
                note_StayG = false;
            else if (current_Button == ButtonType.ButtonP)
                note_StayP = false;
        }
        CheckNote = true;
    }

    #endregion
}
