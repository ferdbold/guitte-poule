using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tendresse.Data;
using Tendresse.Date;


namespace Tendresse.Date {
    public struct DateEvent {
        string question;
        string answer;
        TendresseData image;

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
    public int currentDateEvent = 0; //Current Date event

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
        Debug.Log("TODO : Confirm Entry");
    }

    /// <summary>
    /// Execute a complete Date Event
    /// </summary>
    /// <param name="dateEvent"></param>
    public void ExecuteDateEvent(DateEvent dateEvent) {

    }


    /// <summary>
    /// Returns where you are the first player for this date event.
    /// Player 1 is first for even numbered events
    /// Player 2 is first for un-even numbered events
    /// </summary>
    /// <returns></returns>
    public bool IAmFirst() {
        if (currentDateEvent % 2 == 0) {
            if (GameManager.instance.isFirst == true) return true;
            else return false;
        } else {
            if (GameManager.instance.isFirst == true) return false;
            else return true;
        }
    }

}
