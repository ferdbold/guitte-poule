using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessageView : MonoBehaviour {

    [Header("Components")]
    [SerializeField]
    private Button titlePanel;
    [SerializeField]
    private Button closeButton;

    [Header("Animation")]
    [SerializeField]
    private float slideAnimDuration = 1.0f;

    private TweenParams slideAnimParams;

    private RectTransform rectTransform;
    private float titlePanelHeight;
    private float pivotY;

    public void Awake() {
        this.rectTransform = GetComponent<RectTransform>();

        // Cache default anim values
        this.titlePanelHeight = this.rectTransform.anchoredPosition.y;
        this.pivotY = this.rectTransform.pivot.y;

        // Set anim settings
        this.slideAnimParams = new TweenParams().SetEase(Ease.InOutCubic);
    }

    public void Update() {
        // Update pivot
        Vector2 newPivot = new Vector2(0, this.pivotY);
        this.rectTransform.pivot = newPivot;
    }

    /// <summary>
    /// Shows the message view by sliding it up.
    /// </summary>
    public void SlideUp() {
        this.rectTransform.DOAnchorPos(new Vector2(0, 0), this.slideAnimDuration)
                          .SetAs(this.slideAnimParams);
        DOTween.To(() => this.pivotY, x => this.pivotY = x, 0, this.slideAnimDuration)
               .SetAs(this.slideAnimParams);

        this.titlePanel.interactable = false;
        this.DisplayCloseButton();
    }

    /// <summary>
    /// Hides the message view by sliding it down.
    /// </summary>
    public void SlideDown() {
        this.rectTransform.DOAnchorPos(new Vector2(0, this.titlePanelHeight), this.slideAnimDuration)
                          .SetAs(this.slideAnimParams);
        DOTween.To(() => this.pivotY, x => this.pivotY = x, 1, this.slideAnimDuration)
               .SetAs(this.slideAnimParams);

        this.titlePanel.interactable = true;
        this.HideCloseButton();
    }

    private void DisplayCloseButton() {
        this.closeButton.interactable = true;
        this.closeButton.GetComponent<CanvasGroup>().DOFade(1, this.slideAnimDuration)
            .SetAs(this.slideAnimParams);
    }

    private void HideCloseButton() {
        this.closeButton.interactable = false;
        this.closeButton.GetComponent<CanvasGroup>().DOFade(0, this.slideAnimDuration)
            .SetAs(this.slideAnimParams);
    }
}
