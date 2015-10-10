using UnityEngine;
using UnityEngine.UI;
using Tendresse.Date;

public class HUD : MonoBehaviour {

    [Header("Components")]
    [SerializeField]
    private StoryView storyView;
    [SerializeField]
    private MessageView messageView;

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
