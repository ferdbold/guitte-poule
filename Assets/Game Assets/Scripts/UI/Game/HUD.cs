using UnityEngine;
using UnityEngine.UI;
using Tendresse.Date;

public class HUD : MonoBehaviour {

    [Header("Components")]
    [SerializeField]
    private StoryView storyView;
    [SerializeField]
    private MessageView messageView;

    public void Awake() {
        this.AdaptToScreenSize();
    }

    public void Start() {

    }

    /// <summary>
    /// Adjusts the size of elements so it fits all phone screen sizes.
    /// </summary>
    private void AdaptToScreenSize() {
        RectTransform storyViewRectTransform = this.storyView.GetComponent<RectTransform>(),
                      messageViewRectTransform = this.messageView.GetComponent<RectTransform>();
        Vector2 storyViewSize = storyViewRectTransform.sizeDelta,
                messageViewSize = messageViewRectTransform.sizeDelta;

        storyViewSize.y = Screen.height;
        messageViewSize.y = Screen.height;

        storyViewRectTransform.sizeDelta = storyViewSize;
        messageViewRectTransform.sizeDelta = messageViewSize;
    }

    /// <summary>
    /// Called the date begins
    /// </summary>
    public void Event_OnBeginDate() {
        this.storyView.OnBeginDate();
    }

    /// <summary>
    /// Called when a new event is started
    /// </summary>
    public void Event_OnBeginEvent() {
        this.storyView.OnNewDateEvent(DateManager.instance.GetCurrentEvent());
        this.messageView.OnNewDateEvent(DateManager.instance.GetCurrentEvent());
    }

    /// <summary>
    /// Called when your partner end his drawing
    /// </summary>
    public void Event_OnPartnerFinishDrawing() {
        if (!DateManager.instance.IAmFirst()) {
            //drawBlock.SetActive(false);
            //inputField.gameObject.SetActive(true);
        }
    }
}
