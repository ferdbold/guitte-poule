using UnityEngine;
using System.Collections;

public class MainMenuUI : MonoBehaviour {

    [SerializeField]
    private GameObject[] cloudRings;

    public void OnClick_StartGame() {
        GameManager.instance.SwitchScene(GameManager.Scenes.LoadingGame);
        MainMenuRefUI mainRef = GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>();
        mainRef.HidePlayButton();
        mainRef.HideIntroButton();
        foreach(GameObject ring in cloudRings) {
            ring.SetActive(false);
        }
    }
}
