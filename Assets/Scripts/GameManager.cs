using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    static public GameManager instance;

    void Awake() {
        if (instance == null) {
            instance = this;
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
        Main
    }

    //FUNCTIONS

    public void SwitchScene(Scenes scene) {
        currentScene = scene;
        OnSceneStartup(scene);
    }

    void OnSceneStartup(Scenes scene) {
        switch (scene) {
            case Scenes.Menu:
                break;
            case Scenes.Main:
                break;
        }
    }

//-----------------------------------------------------------------------------------------------------------------------
////////////////////////////////////////////////              ///////////////////////////////////////////////////////////

}
