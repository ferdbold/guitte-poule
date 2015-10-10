using UnityEngine;
using System.Collections;

public class MainMenuUI : MonoBehaviour {

//-----------------------------------------------------------------------------------------------------------------------
////////////////////////////////////////////////   ON CLICK   ///////////////////////////////////////////////////////////

    public void OnClick_StartGame() {
        GameManager.instance.SwitchScene(GameManager.Scenes.LoadingGame);
    }
	
}
