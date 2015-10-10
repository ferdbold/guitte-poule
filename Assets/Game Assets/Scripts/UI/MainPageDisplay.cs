using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainPageDisplay : MonoBehaviour {

    [Header("Title")]
    public Text dateTitle;

    [Header("Intro")]
    public Text introductionText;

    [Header("Date")]
    public Text TextToFill; //The part of text that has a blank space
    public Image drawZone; //The zone for the drawing
    public InputField inputField;
}
