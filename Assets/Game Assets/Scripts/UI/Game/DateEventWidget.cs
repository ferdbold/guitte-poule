using UnityEngine;
using UnityEngine.UI;

public class DateEventWidget : MonoBehaviour {

    [Header("Components")]
    [SerializeField]
    private Text questionText;
    [SerializeField]
    private Image drawing;
    [SerializeField]
    private Button audioPlayButton;
    [SerializeField]
    private Text answerText;

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
