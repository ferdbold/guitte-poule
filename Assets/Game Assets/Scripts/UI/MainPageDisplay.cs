using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Tendresse.Date;

public class MainPageDisplay : MonoBehaviour {

    [Header("Title")]
    public Text dateTitle;

    [Header("Date")]
    public GameObject dateObject;
    public Text TextToFill; //The part of text that has a blank space
    public Image drawZone; //The zone for the drawing
    public GameObject drawBlock; //The image that blocks the drawing zone when the partner is drawing
    public InputField inputField;
    public GameObject confirmButton;

    /// <summary>
    /// Called the date begins
    /// </summary>
    public void Event_OnBeginDate() {
        DateStructure date = DateManager.instance.GetCurrentDate();

        dateTitle.text = "Date " + date.relationLevel + " : " + date.theme;
        TextToFill.text = date.intro;
    }

    /// <summary>
    /// Called when a new event is started
    /// </summary>
    public void Event_OnBeginEvent() {

        DateManager dateManager = DateManager.instance;

        //Title and Question
        TextToFill.text = " \n" + dateManager.GetCurrentEvent().question;
    }

    /// <summary>
    /// Called when your partner end his drawing
    /// </summary>
    public void Event_OnPartnerFinishDrawing() {
        if (!DateManager.instance.IAmFirst()) {
            drawBlock.SetActive(false);
            inputField.gameObject.SetActive(true);
        }
    }

//------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// On Click when the player confirms his choice
    /// </summary>
    public void OnClick_Confirm() {
        DateManager.instance.SendMessage_OnConfirm();
    }

    /// <summary>
    /// On Click when the player runs from the date
    /// </summary>
    public void OnClick_Run() {

    }

//------------------------------------------------------------------------------------------------------------------------------------

    
}
