using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Tendresse.Date;

public class MainPageDisplay : MonoBehaviour {

    [Header("Title")]
    public Text dateTitle;

    [Header("Intro")]
    public GameObject introObject;
    public Text introductionText;

    [Header("Date")]
    public GameObject dateObject;
    public Text TextToFill; //The part of text that has a blank space
    public Image drawZone; //The zone for the drawing
    public GameObject drawBlock; //The image that blocks the drawing zone when the partner is drawing
    public InputField inputField;
    public GameObject confirmButton;

    [Header("Recap")]
    public GameObject recapObject;

    public void Event_StartIntro() {
        dateObject.SetActive(false);
        recapObject.SetActive(false);
        introObject.SetActive(true);

        DateManager dateManager = DateManager.instance;
        dateTitle.text = dateManager.GetCurrentDate().theme;
        introductionText.text = dateManager.GetCurrentDate().intro;
    }

    public void Event_NewEvent() {
        dateObject.SetActive(true);
        introObject.SetActive(false);
        recapObject.SetActive(false);

        DateManager dateManager = DateManager.instance;

        //Title and Question
        introductionText.text = dateManager.GetCurrentEvent().question;
        inputField.gameObject.SetActive(false);

        //Draws
        if (dateManager.IAmFirst()) { 
            drawBlock.SetActive(false);
            return;
        }

        //Else writes answer 
        drawBlock.SetActive(true);
    }

    /// <summary>
    /// Event when the players drawing finishes and gives his turn to the next player
    /// </summary>
    public void Event_OnPartnerFinishDrawing() {
        if (!DateManager.instance.IAmFirst()) {
            drawBlock.SetActive(false);
            inputField.gameObject.SetActive(true);
        }
    }

    public void Event_StartRecap() {
        dateObject.SetActive(false);
        introObject.SetActive(false);
        recapObject.SetActive(true);
    }

//------------------------------------------------------------------------------------------------------------------------------------

    public void OnClick_FindLove() {
        DateManager.instance.SendMessage_OnConfirm();
    }

    public void OnClick_Run() {

    }

//------------------------------------------------------------------------------------------------------------------------------------

    
}
