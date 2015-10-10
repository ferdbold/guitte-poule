using UnityEngine;
using System.Collections;

public class MainMenuUI : MonoBehaviour {

    [SerializeField]
    private GameObject[] cloudRings;

    public void OnClick_StartGame() {
        GameManager.instance.SwitchScene(GameManager.Scenes.LoadingGame);
        foreach(GameObject ring in cloudRings) {
            ring.SetActive(false);
        }
    }
}
