using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour {

    public Text loadingText;
    public float timedInterval;

    public void StartAnim() {
        StartCoroutine(LoadingIndicatorAnim());
    }

    IEnumerator LoadingIndicatorAnim() {
        while (true) {
            loadingText.CrossFadeAlpha(0, timedInterval, false);
            yield return new WaitForSeconds(timedInterval);
            loadingText.CrossFadeAlpha(1, timedInterval, false);
            yield return new WaitForSeconds(timedInterval);
        }
    }
}
