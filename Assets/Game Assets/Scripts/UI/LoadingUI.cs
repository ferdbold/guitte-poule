using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour {

    public Text loadingText;
    public float timedInterval;

    public void StartAnim() {
        StartCoroutine(LoadingIndicatorAnim());
        StartCoroutine(TestLoad());
    }

    IEnumerator LoadingIndicatorAnim() {
        while (true) {
            loadingText.CrossFadeAlpha(0, timedInterval, false);
            yield return new WaitForSeconds(timedInterval);
            loadingText.CrossFadeAlpha(1, timedInterval, false);
            yield return new WaitForSeconds(timedInterval);
        }
    }

    IEnumerator TestLoad() {
        yield return new WaitForSeconds(4);
        GameManager.instance.SwitchScene(GameManager.Scenes.Main);
    }
}
