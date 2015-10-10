using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GameManager : MonoBehaviour {

    static public GameManager instance;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    void OnDestroy() {
        if (instance = this) {
            instance = null;
        }
    }

//-----------------------------------------------------------------------------------------------------------------------
//////////////////////////////////////////////// SWITCH SCENE ///////////////////////////////////////////////////////////
    
    //VARIABLES
    Scenes currentScene;

    public enum Scenes {
        Menu,
        LoadingGame,
        Main
    }

    //FUNCTIONS

    public void SwitchScene(Scenes scene) {
        OnSceneEnd(currentScene);
        currentScene = scene;
        OnSceneStartup(currentScene);
    }

    void OnSceneEnd(Scenes scene) {
        switch (scene) {
            case Scenes.Menu:
                GameObject.FindWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().mainMenu.DOFade(0, 0.75f);
                break;
            case Scenes.LoadingGame:
                CanvasGroup loading = GameObject.FindWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().loading;
                loading.DOFade(0, 0.75f);
                loading.GetComponent<LoadingUI>().StopAllCoroutines();
                break;
            case Scenes.Main:
                break;
        }
    }

    void OnSceneStartup(Scenes scene) {
        switch (scene) {
            case Scenes.Menu:
                GameObject.FindWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().mainMenu.DOFade(1, 0.75f);
                break;
            case Scenes.LoadingGame:
                CanvasGroup loading = GameObject.FindWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().loading;
                loading.DOFade(1, 0.75f);
                loading.GetComponent<LoadingUI>().StartAnim();
                break;
            case Scenes.Main:
                Application.LoadLevel("Main");
                break;
        }
    }

//-----------------------------------------------------------------------------------------------------------------------
////////////////////////////////////////////////              ///////////////////////////////////////////////////////////

}
