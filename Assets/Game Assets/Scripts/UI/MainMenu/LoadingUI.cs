using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour {

    [SerializeField]
    private float timedInterval;

    [Header("Components")]
    [SerializeField]
    private Text loadingText;
    [SerializeField]
    private RawImage backgroundVideo;

    private MovieTexture videoTex;

    public void Awake() {
        this.videoTex = (MovieTexture)this.backgroundVideo.mainTexture;
        this.videoTex.loop = true;
    }
    
    public void StartAnim() {
        StartCoroutine(LoadingIndicatorAnim());
        this.videoTex.Play();
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
