using UnityEngine;
using UnityEngine.UI;

public class DateEventWidget : MonoBehaviour, ISoundView {

    [Header("Components")]
    [SerializeField]
    private Text questionText;
    [SerializeField]
    private Image drawing;
    [SerializeField]
    private SoundButton audioPlayButton;
    [SerializeField]
    private Text answerText;

    public void Awake() {
        audioPlayButton.ParentView = this;
    }

    public void OnSoundButtonClick() {

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
}
