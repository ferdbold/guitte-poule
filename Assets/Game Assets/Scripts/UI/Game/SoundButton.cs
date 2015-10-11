using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SoundButton : MonoBehaviour {

    public enum SoundButtonMode {
        RECORD,
        PLAY,
        DELETE
    };
    
    [Header("Components")]
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Slider playbackSlider;
    [SerializeField]
    private CanvasGroup canvasGroup;
    private Button button;

    [Header("Colors")]
    [SerializeField]
    private Color recordColor;
    [SerializeField]
    private Color playColor;
    [SerializeField]
    private Color deleteColor;

    [Header("Icons")]
    [SerializeField]
    private Sprite recordIcon;
    [SerializeField]
    private Sprite playIcon;
    [SerializeField]
    private Sprite deleteIcon;


    public void Awake() {
        this.button = GetComponent<Button>();
    }

    /// <summary>
    /// Handle clicks
    /// </summary>
    public void OnClick() {
        this.button.interactable = false;
        this.playbackSlider.DOValue(1, 3)
                           .SetEase(Ease.Linear)
                           .OnComplete(this.OnPlaybackComplete);
    }

    public void OnPlaybackComplete() {
        this.button.interactable = true;
        this.playbackSlider.value = 0;
    }

    /// <summary>
    /// The button mode.
    /// </summary>
    public SoundButtonMode Mode {
        set {
            ColorBlock colors = this.button.colors;
            
            switch(value) {
                case SoundButtonMode.RECORD:
                    this.icon.sprite = this.recordIcon;
                    colors.normalColor = this.recordColor;
                    colors.highlightedColor = this.recordColor;
                    break;
                case SoundButtonMode.PLAY:
                    this.icon.sprite = this.playIcon;
                    colors.normalColor = this.playColor;
                    colors.highlightedColor = this.playColor;
                    break;
                case SoundButtonMode.DELETE:
                    this.icon.sprite = this.deleteIcon;
                    colors.normalColor = this.deleteColor;
                    colors.highlightedColor = this.deleteColor;
                    break;
            }

            this.button.colors = colors;
        }
    }

    public bool Enabled {
        set {
            this.button.interactable = value;
            if (value) {
                this.canvasGroup.alpha = 1;
            } else {
                this.canvasGroup.alpha = 0;
            }
        }
    }
}
