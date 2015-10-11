using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class MainMenuRefUI : MonoBehaviour {

    public CanvasGroup mainMenu;
    public CanvasGroup loading;
    public CanvasGroup playButton;
    public CanvasGroup IntroButton;
    public CanvasGroup IntroText;
    public CanvasGroup sky;

    public void Start() {
        
    }

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
        IntroButton.GetComponentInChildren<Button>().interactable = true;
    }

    public void HideIntroButton() {
        IntroButton.DOFade(0, 1);
        IntroButton.GetComponentInChildren<Button>().interactable = false;
    }

    public void ShowIntroText() {
        IntroText.DOFade(1, 1);
    }

    public void HideIntroText() {
        IntroText.DOFade(0, 1);
    }

    public void ShowMainMenu() {
        mainMenu.DOFade(1, 1);
    }

    public void HideMainMenu() {
        mainMenu.DOFade(0, 1);
    }

    public void ShowSky() {
        sky.DOFade(1, 1);
    }

    public void HideSky() {
        sky.DOFade(0, 1);
    }


    public void TransitionToSky() {
        sky.transform.DOMoveY(0, 3f);
        mainMenu.transform.DOMoveY(-12, 3f);
        //loading.transform.DOMoveY(-12, 3f);
    }
}
