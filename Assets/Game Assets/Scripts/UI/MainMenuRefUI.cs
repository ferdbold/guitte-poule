using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class MainMenuRefUI : MonoBehaviour {

    public CanvasGroup mainMenu;
    public CanvasGroup loading;
    public CanvasGroup playButton;
    public CanvasGroup IntroButton;

    public void ShowPlayButton() {
        playButton.DOFade(1, 1);
        playButton.GetComponentInChildren<Button>().interactable = true;
    }

    public void HidePlayButton() {
        playButton.DOFade(0, 1);
        playButton.GetComponentInChildren<Button>().interactable = false;
    }

    public void ShowIntroButton() {
        IntroButton.DOFade(1, 1);
        playButton.GetComponentInChildren<Button>().interactable = true;
    }

    public void HideIntroButton() {
        IntroButton.DOFade(0, 1);
        playButton.GetComponentInChildren<Button>().interactable = false;
    }
}
