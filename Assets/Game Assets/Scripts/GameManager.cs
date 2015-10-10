using UnityEngine;
using System.Collections;
using DG.Tweening;
using Message;

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

    /// <summary>
    /// Switch between scenes
    /// </summary>
    /// <param name="scene"></param>
    public void SwitchScene(Scenes scene) {
        OnSceneEnd(currentScene);
        currentScene = scene;
        OnSceneStartup(currentScene);
    }

    /// <summary>
    /// Event when the scene end
    /// </summary>
    /// <param name="scene"></param>
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

    /// <summary>
    /// Event on scene startup
    /// </summary>
    /// <param name="scene"></param>
    void OnSceneStartup(Scenes scene) {
        switch (scene) {
            case Scenes.Menu:
                Application.LoadLevel("Menu");
                break;
            case Scenes.LoadingGame:
                CanvasGroup loading = GameObject.FindWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().loading;
                loading.DOFade(1, 0.75f);
                loading.GetComponent<LoadingUI>().StartAnim();
                message messa = new message("queueMatch");
                NetManager.instance.SendMessage(messa);
                break;
            case Scenes.Main:
                Application.LoadLevel("Main");
                break;
        }
    }

//-----------------------------------------------------------------------------------------------------------------------
////////////////////////////////////////////////              ///////////////////////////////////////////////////////////

    /// <summary>
    /// Event when the player finds a partner online
    /// </summary>
    public void Event_OnFindPartner() {
        SwitchScene(Scenes.Main);
    }
}
