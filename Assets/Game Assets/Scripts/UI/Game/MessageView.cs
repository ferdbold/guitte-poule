using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Tendresse.Date;
using System.Collections;

public class MessageView : MonoBehaviour, ISoundView {

    [Header("Components")]
    [SerializeField]
    private Button titlePanel;
    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Button closeButton;
    [SerializeField]
    private Button sendButton;
    [SerializeField]
    private Text questionText;
    [SerializeField]
    private InputField answerInput;
    [SerializeField]
    private TouchDraw drawZone;
    [SerializeField]
    private SoundButton soundButton;
    [SerializeField]
    private Button deleteSoundButton;

    [Header("Animation")]
    [SerializeField]
    private float slideAnimDuration = 1.0f;

    [Header("Text")]
    [SerializeField]
    private string youDrawTitleText = "C'est à vous de dessiner!";
    [SerializeField]
    private string youTalkTitleText = "C'est à vous de parler!";
    [SerializeField]
    private string youWriteTitleText = "C'est à vous d'écrire!";
    [SerializeField]
    private string theyDrawTitleText = "C'est à l'autre de dessiner...";
    [SerializeField]
    private string theyTalkTitleText = "C'est à l'autre de parler...";
    [SerializeField]
    private string theyWriteTitleText = "C'est à l'autre d'écrire...";

    private TweenParams slideAnimParams;

    private RectTransform rectTransform;
    private float titlePanelHeight;
    private float pivotY;
    private float drawZoneY;

    /// <summary>
    /// Whether or not a sound has been recorded by you this phase.
    /// </summary>
    private bool soundRecorded;

    public void Awake() {
        this.rectTransform = GetComponent<RectTransform>();

        // Cache default anim values
        this.titlePanelHeight = this.rectTransform.anchoredPosition.y;
        this.pivotY = this.rectTransform.pivot.y;
        this.drawZoneY = this.drawZone.transform.position.y;

        // Set anim settings
        this.slideAnimParams = new TweenParams().SetEase(Ease.InOutExpo);

        // Set sound button parent reference
        this.soundButton.ParentView = this;
    }

    public void Update() {
        // Update pivot
        Vector2 newPivot = new Vector2(0, this.pivotY);
        this.rectTransform.pivot = newPivot;
    }

    /// <summary>
    /// Handle new date events
    /// </summary>
    /// <param name="dateEvent">The new date event</param>
    /// <param name="first">If this player is first at drawing/speaking</param>
    public void OnNewDateEvent(DateEvent dateEvent, bool first) {
        this.questionText.text = dateEvent.question;

        if (first) {
            if (dateEvent.mediaIsDrawing) {
                this.titleText.text = this.youDrawTitleText;
                this.SetupDrawing();
                this.titlePanel.interactable = true;
            } else {
                this.titleText.text = this.youTalkTitleText;
                this.SetupRecording();
                this.titlePanel.interactable = true;
            }
        } else {
            if (dateEvent.mediaIsDrawing) {
                this.titleText.text = this.theyDrawTitleText;
                this.titlePanel.interactable = false;
            } else {
                this.titleText.text = this.theyTalkTitleText;
                this.titlePanel.interactable = false;
            }
        }
    }

    /// <summary>
    /// Handle new media received events
    /// </summary>
    /// <param name="dateEvent">The new date event</param>
    /// <param name="first">Is this player the first to play</param>
    public void OnReceivedMediaEvent(DateEvent dateEvent, bool first) {
        if (first) {
            this.titleText.text = this.theyWriteTitleText;
            this.titlePanel.interactable = false;
        } else {
            this.titleText.text = this.youWriteTitleText;
            this.SetupWriting(dateEvent.mediaIsDrawing);
            this.titlePanel.interactable = true;
        }
    }

    /// <summary>
    /// Shows the message view by sliding it up.
    /// </summary>
    private void SlideUp() {
        DOTween.To(() => this.pivotY, x => this.pivotY = x, 0.1f, this.slideAnimDuration)
               .SetAs(this.slideAnimParams);
        this.drawZone.transform.DOMoveY(0, this.slideAnimDuration)
                               .SetAs(this.slideAnimParams);

        this.titlePanel.interactable = false;
        this.ShowTitleButtons();
    }

    /// <summary>
    /// Hides the message view by sliding it down.
    /// </summary>
    private void SlideDown() {
        DOTween.To(() => this.pivotY, x => this.pivotY = x, 1.0f, this.slideAnimDuration)
               .SetAs(this.slideAnimParams);
        this.drawZone.transform.DOMoveY(this.drawZoneY, this.slideAnimDuration)
                               .SetAs(this.slideAnimParams);

        this.titlePanel.interactable = true;
        this.HideTitleButtons();
    }

    private void ShowTitleButtons() {
        this.closeButton.interactable = true;
        this.closeButton.GetComponent<CanvasGroup>().DOFade(1, this.slideAnimDuration)
            .SetAs(this.slideAnimParams);

        this.sendButton.interactable = true;
        this.sendButton.GetComponent<CanvasGroup>().DOFade(1, this.slideAnimDuration)
            .SetAs(this.slideAnimParams);
    }

    private void HideTitleButtons() {
        this.closeButton.interactable = false;
        this.closeButton.GetComponent<CanvasGroup>().DOFade(0, this.slideAnimDuration)
            .SetAs(this.slideAnimParams);

        this.sendButton.interactable = false;
        this.sendButton.GetComponent<CanvasGroup>().DOFade(0, this.slideAnimDuration)
            .SetAs(this.slideAnimParams);
    }

    /// <summary>
    /// Set up the drawing mode.
    /// </summary>
    private void SetupDrawing() {
        this.drawZone.canDraw = true;
        this.soundButton.Enabled = false;
        this.DeleteSoundButtonEnabled = false;
        this.AnswerInputEnabled = false;
    }

    private void SetupRecording() {
        this.drawZone.canDraw = false;
        this.soundButton.Enabled = true;
        this.DeleteSoundButtonEnabled = false;
        this.AnswerInputEnabled = false;
        this.soundButton.Mode = SoundButton.SoundButtonMode.RECORD;

        this.soundRecorded = false;
    }

    /// <summary>
    /// Set up the writing mode
    /// </summary>
    /// <param name="mediaIsDrawing">True if media is a drawing, false if media is a sound</param>
    private void SetupWriting(bool mediaIsDrawing) {
        this.drawZone.canDraw = false;
        this.soundButton.Enabled = !mediaIsDrawing;
        this.DeleteSoundButtonEnabled = false;
        this.AnswerInputEnabled = true;

        if (!mediaIsDrawing) {
            this.soundButton.Mode = SoundButton.SoundButtonMode.PLAY;
        }
    }

    /// <summary>
    /// Handle close button click
    /// </summary>
    public void OnClose() {
        this.SlideDown();
    }

    /// <summary>
    /// Handle send button click
    /// </summary>
    public void OnSend() {
        DateManager.instance.SendMessage_OnConfirmHandle(this.answerInput.text);
        this.SlideDown();
    }

    /// <summary>
    /// Handle sound button clicks
    /// </summary>
    public void OnSoundButtonClick() {
        DateEvent dateEvent = DateManager.instance.GetCurrentEvent();
        
        if (this.soundRecorded) {
            AudioManager.instance.PlaySoundFromMessage(AudioManager.instance.getRecordMessage());
        } else {
            StartCoroutine(this.RecordSound());
        }
    }

    public IEnumerator RecordSound() {
        AudioManager.instance.startRecord();
        yield return new WaitForSeconds(3);
        DateManager.instance.SetCurrentEventSound(AudioManager.instance.MakeMessageFromClip());
        this.soundButton.Mode = SoundButton.SoundButtonMode.PLAY;
        this.DeleteSoundButtonEnabled = true;
    }

    /// <summary>
    /// Handle delete sound button clicks
    /// </summary>
    public void OnDeleteSoundButtonClick() {
        DateManager.instance.SetCurrentEventSound(new Message.message(""));
        this.soundButton.Mode = SoundButton.SoundButtonMode.RECORD;
        this.soundRecorded = false;
        this.DeleteSoundButtonEnabled = false;
    }

    private bool AnswerInputEnabled {
        set {
            CanvasGroup answerInputCanvasGroup = this.answerInput.GetComponent<CanvasGroup>();

            this.answerInput.interactable = value;
            answerInputCanvasGroup.alpha = (value) ? 1 : 0;
        }
    }

    private bool DeleteSoundButtonEnabled {
        set {
            CanvasGroup deleteSoundButtonCanvasGroup = this.deleteSoundButton.GetComponent<CanvasGroup>();

            this.deleteSoundButton.interactable = value;
            deleteSoundButtonCanvasGroup.DOFade((value) ? 1 : 0, 0.5f);
        }
    }
}
