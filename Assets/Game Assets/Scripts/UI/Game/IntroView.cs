using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IntroView : MonoBehaviour {

    [Header("Components")]
    [SerializeField]
    private InputField partnerNameInput;
    [SerializeField]
    private Button confirmButton;

    [Header("Animation")]
    [SerializeField]
    private float slideAnimDuration = 1;

    [Header("Texts")]
    [SerializeField]
    private string waitingForPartner = "En attente de votre chéri...";

    private RectTransform rectTransform;
    private TweenParams slideAnimParams;
    private float pivotY;

    public void Awake() {
        // Cache default anim values
        this.rectTransform = GetComponent<RectTransform>();
        this.pivotY = this.rectTransform.pivot.y;

        // Set anim settings
        this.slideAnimParams = new TweenParams().SetEase(Ease.InOutExpo);
    }

    public void Update() {
        // Update pivot
        Vector2 newPivot = new Vector2(0, this.pivotY);
        this.rectTransform.pivot = newPivot;
    }

    /// <summary>
    /// Handle begin date events
    /// </summary>
    public void OnBeginDate() {
        this.SlideUp();
    }

    /// <summary>
    /// Handle partner name input changes
    /// </summary>
    public void OnPartnerNameChanged() {
        this.confirmButton.interactable = (this.partnerNameInput.text != "");
    }

    /// <summary>
    /// Handle confirm button clicks
    /// </summary>
    public void OnConfirm() {
        DateManager.instance.SendMessage_OnConfirm(this.partnerNameInput.text);
        this.partnerNameInput.interactable = false;
        this.partnerNameInput.text = this.waitingForPartner;
    }

    /// <summary>
    /// Slide this widget out of view
    /// </summary>
    private void SlideUp() {
        DOTween.To(() => this.pivotY, x => this.pivotY = x, 0, this.slideAnimDuration)
               .SetAs(this.slideAnimParams);
    }
}
