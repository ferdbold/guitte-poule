using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Tendresse.Date;

public class MessageView : MonoBehaviour {

    [Header("Components")]
    [SerializeField]
    private Button titlePanel;
    [SerializeField]
    private Button closeButton;
    [SerializeField]
    private Button sendButton;
    [SerializeField]
    private Text questionText;
    [SerializeField]
    private TouchDraw drawZone;

    [Header("Animation")]
    [SerializeField]
    private float slideAnimDuration = 1.0f;

    private TweenParams slideAnimParams;

    private RectTransform rectTransform;
    private float titlePanelHeight;
    private float pivotY;
    private float drawZoneY;

    public void Awake() {
        this.rectTransform = GetComponent<RectTransform>();

        // Cache default anim values
        this.titlePanelHeight = this.rectTransform.anchoredPosition.y;
        this.pivotY = this.rectTransform.pivot.y;
        this.drawZoneY = this.drawZone.transform.position.y;

        // Set anim settings
        this.slideAnimParams = new TweenParams().SetEase(Ease.InOutExpo);
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
    public void OnNewDateEvent(DateEvent dateEvent) {
        this.questionText.text = dateEvent.question;
    }

    /// <summary>
    /// Shows the message view by sliding it up.
    /// </summary>
    private void SlideUp() {
        this.rectTransform.DOAnchorPos(new Vector2(0, 0), this.slideAnimDuration)
                          .SetAs(this.slideAnimParams);
        DOTween.To(() => this.pivotY, x => this.pivotY = x, 0, this.slideAnimDuration)
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
        this.rectTransform.DOAnchorPos(new Vector2(0, this.titlePanelHeight), this.slideAnimDuration)
                          .SetAs(this.slideAnimParams);
        DOTween.To(() => this.pivotY, x => this.pivotY = x, 1, this.slideAnimDuration)
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

    public void OnClose() {
        this.SlideDown();
    }

    public void OnSend() {
        this.SlideDown();
        DateManager.instance.OnConfirmEntry();
    }
}
