using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tendresse.Data;

public class DateManager : MonoBehaviour {

    static public DateManager instance;

    public GameObject DrawingObjectPrefab;

    [SerializeField]
    private TouchDraw mainTouchDraw;
    private List<TouchDraw> tempDrawingList = new List<TouchDraw>();

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
}
