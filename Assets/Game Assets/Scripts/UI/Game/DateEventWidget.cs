using UnityEngine;
using UnityEngine.UI;

public class DateEventWidget : MonoBehaviour, ISoundView {

    [Header("Components")]
    [SerializeField]
    private Text questionText;
    [SerializeField]
    private RectTransform drawingPoint;
    [SerializeField]
    private SoundButton audioPlayButton;
    [SerializeField]
    private Text answerText;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject drawingPrefab;

    private Transform drawZoneContainer;
    private TouchDraw drawing;
    private Tendresse.Data.TendresseData drawingData;
    private bool drawDrawing = false;
    
    private Tendresse.Date.DateEvent dateEvent;

    public void Awake() {
        this.drawZoneContainer = GameObject.Find("StoryDrawZones").transform;

        // Create and store draw zone
        GameObject drawZoneObject = (GameObject)GameObject.Instantiate(this.drawingPrefab, Vector3.zero, Quaternion.identity);
        drawZoneObject.transform.parent = this.drawZoneContainer;
        this.drawing = drawZoneObject.GetComponent<TouchDraw>();
        this.drawing.canDraw = false;

        // Place draw zone under drawing point
        this.drawing.transform.position = this.drawingPoint.TransformPoint(this.drawingPoint.rect.center);

        // Set up sound button
        this.audioPlayButton.ParentView = this;
        this.audioPlayButton.Mode = SoundButton.SoundButtonMode.PLAY;
    }

    public void Update() {
        this.drawing.transform.position = this.drawingPoint.TransformPoint(this.drawingPoint.rect.center);
        
        // Refresh drawing
        if (this.drawDrawing) {
            this.drawing.WipeDrawData();
            this.drawing.LoadTendresseData(this.drawingData, this.drawing.transform.position, 1);
        }
    }

    /// <summary>
    /// Load a drawing into this widget's drawing zone
    /// </summary>
    /// <param name="drawing">The drawing data</param>
    public void DisplayDrawing(Tendresse.Data.TendresseData drawing) {
        this.drawingData = drawing;
        this.drawDrawing = true;
    }

    public void OnSoundButtonClick() {
        AudioManager.instance.PlaySoundFromMessage(this.dateEvent.sound);
    }

    /* PROPERTIES */

    public string Question {
        set {
            this.questionText.text = value;
        }
    }

    public string Answer {
        set {
            this.answerText.text = value;
        }
    }

    public bool DrawZoneEnabled {
        set {
            // TODO
        }
    }

    public bool SoundButtonEnabled {
        set {
            this.audioPlayButton.GetComponent<Button>().interactable = value;
            this.audioPlayButton.GetComponent<CanvasGroup>().alpha = (value) ? 1 : 0;
        }
    }

    public Tendresse.Date.DateEvent DateEvent {
        set {
            this.dateEvent = value;
        }
    }
}
