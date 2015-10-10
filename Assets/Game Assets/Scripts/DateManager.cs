using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tendresse.Data;
using Tendresse.Date;


namespace Tendresse.Date {
    //Date Event
    public struct DateEvent {
        string question; //Question to be asked
        string answer; //Stored answer written for the question
        TendresseData image; //Store image drawn for the question

        DateEvent(TendresseData TD, string q, string a) {
            image = TD;
            question = q;
            answer = a;
        }
    }
 
}




public class DateManager : MonoBehaviour {

    static public DateManager instance;

    [Header("Date Management")]
    public List<DateEvent> DateEvents; //List of date events to execute
    private int _currentDateEvent = 0; //Current Date event
    private int _currentStepInDate = 0; //Current Step in the date ( 0 = first player draws , 1 = second player writes, 2 = text resolution, then start next )
    private bool _canUseConfirmButton = false;

    [Header("Draw Zones")]
    public GameObject DrawingObjectPrefab;
    public TouchDraw mainTouchDraw; //Main space to draw. This is where the player draws.
    private List<TouchDraw> tempDrawingList = new List<TouchDraw>(); //Temporary image shown, used to show drawings but not to draw !

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void OnDestroy() {
        if (instance = this) {
            instance = null;
        }
    }


    void Update() {
        DebugMethods();
    }

    /////////////////////////// MAKE DRAWINGS ////////////////////////

    /// <summary>
    /// Draw a temporary drawing into a newly created gameobject
    /// </summary>
    /// <param name="tData"></param>
    /// <param name="imagePosition"></param>
    /// <param name="imageScale"></param>
    public void DrawTempImageAt(TendresseData tData, Vector3 imagePosition, float imageScale) {
        GameObject go = (GameObject)Instantiate(DrawingObjectPrefab, imagePosition, Quaternion.identity);
        TouchDraw touchDraw = go.GetComponent<TouchDraw>();
        touchDraw.canDraw = false; //Cant draw in temporaty touch  <--- Yes, there is a typo, too lazy to fix. Yes Am Tired. KthxBye.
        touchDraw.LoadTendresseData(tData, imagePosition, imageScale);

        tempDrawingList.Add(touchDraw);
    }

    /// <summary>
    /// Delete all temporary drawings 
    /// </summary>
    public void DeleteTempImage() {
        for (int i = 0; i < tempDrawingList.Count; i++) {
            Destroy(tempDrawingList[i].gameObject);
        }
        tempDrawingList = new List<TouchDraw>();
    }

    /// <summary>
    /// Draw into the main image Draw
    /// </summary>
    /// <param name="tData"></param>
    /// <param name="imagePosition"></param>
    /// <param name="imageScale"></param>
    public void DrawImageAt(TendresseData tData, Vector3 imagePosition, float imageScale) {
        if (mainTouchDraw != null) {
            mainTouchDraw.LoadTendresseData(tData, imagePosition, imageScale);
        }
    }



    private void DebugMethods() {

        if (Input.GetKeyDown(KeyCode.Z)) {
            GameManager.instance.Event_OnSendImage(mainTouchDraw.SaveCurrentData());
        }
        if (Input.GetKeyDown(KeyCode.X)) {

        }
    }

    /////////////////////////////////////////////// Date Management ///////////////////////////////
    /// <summary>
    /// Player has clicked the submit button.
    /// </summary>
    public void OnConfirmEntry() {
        if (_currentStepInDate == 0 && _canUseConfirmButton) { //If first player presses confirm when he draws, start text phase
            ExecuteDateEvent_TextPhase(DateEvents[_currentDateEvent]);
        } else if (_currentStepInDate == 1 && _canUseConfirmButton) { //If second player presses confirm when he writes, start end phase
            ExecuteDateEvent_EndPhase(DateEvents[_currentDateEvent]);
        }
    }

    /// <summary>
    /// Execute a complete Date Event
    /// </summary>
    /// <param name="dateEvent"></param>
    private void ExecuteDateEvent() {
        if (_currentDateEvent < DateEvents.Count) { //If there is still events to do in list
            Debug.Log("Executing Date Event ");
            _currentStepInDate = 0;
            ExecuteDateEvent_DrawPhase(DateEvents[_currentDateEvent]);
        } else {
            Debug.Log("Executed last date event in list. Date over.");
        }
    }

    /// <summary>
    /// Start the draw phase of the event. First player draws while second player wait.
    /// </summary>
    /// <param name="dateEvent"></param>
    private void ExecuteDateEvent_DrawPhase(DateEvent dateEvent) {
        if (IAmFirst()) {
            //TODO : Show Draw box
            _canUseConfirmButton = true;
        } else {
            //TODO : Wait for Other Player Draw Phase
            _canUseConfirmButton = false;
        }
    }

    /// <summary>
    /// Starts the Text Phase of the Event where the second player write about the drawing while the first player waits.
    /// </summary>
    /// <param name="dateEvent"></param>
    private void ExecuteDateEvent_TextPhase(DateEvent dateEvent) {
        if (IAmFirst()) {
            //TODO : Wait for other player Write Phase
            _canUseConfirmButton = false;
        } else {
            //TODO : Show Text Box and Other Player Image
            _canUseConfirmButton = true;
        }
    }


    /// <summary>
    /// Starts the last part of the event where the resolution is shown
    /// </summary>
    /// <param name="dateEvent"></param>
    private void ExecuteDateEvent_EndPhase(DateEvent dateEvent) {
        if (IAmFirst()) {
            //TODO : Wait for next event to start ?
        } else {
            //TODO : Wait for next event to start ?
        }
    }

    /// <summary>
    /// Returns where you are the first player for this date event.
    /// Player 1 is first for even numbered events
    /// Player 2 is first for un-even numbered events
    /// </summary>
    /// <returns></returns>
    public bool IAmFirst() {
        if (_currentDateEvent % 2 == 0) {
            if (GameManager.instance.isFirst == true) return true;
            else return false;
        } else {
            if (GameManager.instance.isFirst == true) return false;
            else return true;
        }
    }

}
