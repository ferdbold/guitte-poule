using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingUI : MonoBehaviour {

    [SerializeField]
    private float timedInterval;

    [Header("Components")]
    [SerializeField]
    private Text loadingText;
    [SerializeField]
    private RawImage backgroundVideo;
    public Sprite[] spriteSheet;
    public Image backgroundObject;
    public Camera cameraRef;
    
    public void StartAnim() {
        StartCoroutine(LoadingIndicatorAnim());
        StartCoroutine(PlayBackGroundAnim());
        cameraRef.transform.DOMoveZ(5, 3f);
        cameraRef.transform.DOMoveY(5, 3f);
        StartCoroutine(SpinAround());
    }

    IEnumerator LoadingIndicatorAnim() {
        while (true) {
            loadingText.CrossFadeAlpha(0, timedInterval, false);
            yield return new WaitForSeconds(timedInterval);
            loadingText.CrossFadeAlpha(1, timedInterval, false);
            yield return new WaitForSeconds(timedInterval);
        }
    }

    IEnumerator PlayBackGroundAnim() {
        while (true) {
            for (int i = 0; i < spriteSheet.Length; i++) {
                backgroundObject.sprite = spriteSheet[i];
                yield return new WaitForSeconds(0.0175f);
            }
        }
    }

    IEnumerator SpinAround() {
        float angle = 180;
        while (true) {
            angle += 0.001f * Time.deltaTime;
            cameraRef.transform.Rotate(new Vector3(0, 0, angle));
            yield return new WaitForEndOfFrame();
        }
    }
}
