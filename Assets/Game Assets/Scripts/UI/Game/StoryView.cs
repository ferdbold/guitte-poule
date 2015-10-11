﻿using UnityEngine;
using UnityEngine.UI;
using Tendresse.Date;

public class StoryView : MonoBehaviour {

    [Header("Components")]
    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Transform dateEventWidgetWrapper;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject dateEventWidgetPrefab;

    [Header("Style")]
    [SerializeField]
    private float dateEventWidgetMargin = 30;
    private float dateEventWidgetHeight;

    public void Awake() {
        this.dateEventWidgetHeight = Screen.height / 2.0f;
    }

    public void OnBeginDate() {
        DateStructure date = DateManager.instance.GetCurrentDate();
        titleText.text = "Date avec " + date.dateName;
    }

    /// <summary>
    /// Handle new date events
    /// </summary>
    /// <param name="dateEvent">The new date event</param>
    public void OnNewDateEvent(DateEvent dateEvent) {
        DateEventWidget newWidget = this.AddNewDateEventWidget(dateEvent);
        newWidget.Question = dateEvent.question;
        newWidget.Answer = "";

    }

    public void OnNewDateEventTextAwnser(DateEvent dateEvent)
    {
        DateEventWidget newWidget = this.AddNewDateEventWidget(dateEvent);
        newWidget.Answer = dateEvent.answer;

    }

    /// <summary>
    /// Handle new media received events
    /// </summary>
    /// <param name="dateEvent"></param>
    public void OnReceivedMediaEvent(DateEvent dateEvent) {

    }

    /// <summary>
    /// Add a new DateEventWidget to the story scroll rect
    /// </summary>
    /// <param name="dateEvent">The new date event</param>
    private DateEventWidget AddNewDateEventWidget(DateEvent dateEvent) {
        RectTransform wrapperRectTransform = this.dateEventWidgetWrapper.GetComponent<RectTransform>();
        Vector2 wrapperSize = wrapperRectTransform.sizeDelta;
        float newWidgetY = -(wrapperSize.y + this.dateEventWidgetMargin);

        // Instantiate the new widget
        GameObject newDateWidget = GameObject.Instantiate(dateEventWidgetPrefab);
        newDateWidget.transform.SetParent(this.dateEventWidgetWrapper);

        // Position the new widget at the bottom of the wrapper
        RectTransform newWidgetRectTransform = newDateWidget.GetComponent<RectTransform>();
        Vector2 newWidgetSize = newWidgetRectTransform.sizeDelta;
        Vector3 newWidgetAnchoredPos = newWidgetRectTransform.anchoredPosition3D;
        Vector2 newWidgetOffsetMin = newWidgetRectTransform.offsetMin,
                newWidgetOffsetMax = newWidgetRectTransform.offsetMax;
        
        newWidgetAnchoredPos.y = newWidgetY;
        newWidgetAnchoredPos.z = 0;
        newWidgetOffsetMin.x = 10;
        newWidgetOffsetMax.x = -10;
        
        newWidgetRectTransform.offsetMin = newWidgetOffsetMin;
        newWidgetRectTransform.offsetMax = newWidgetOffsetMax;
        newWidgetRectTransform.localScale = new Vector3(1, 1, 1);

        newWidgetSize = new Vector2(newWidgetRectTransform.sizeDelta.x, this.dateEventWidgetHeight);
        newWidgetRectTransform.sizeDelta = newWidgetSize;

        newWidgetAnchoredPos.x = newWidgetRectTransform.anchoredPosition.x;
        newWidgetRectTransform.anchoredPosition3D = newWidgetAnchoredPos;

        // Resize the wrapper
        wrapperSize.y += newWidgetRectTransform.sizeDelta.y + this.dateEventWidgetMargin;
        wrapperRectTransform.sizeDelta = wrapperSize;

        return newDateWidget.GetComponent<DateEventWidget>();
    }
}
