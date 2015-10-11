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
        cameraRef.transform.DOMoveZ(4f, 3f);
        cameraRef.transform.DOMoveY(3.5f, 3f);
        StartCoroutine(SpinAround());
        AudioManager.instance.PlayAudioClip("vent");
        AudioManager.instance.SetLoop(true);
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
        Vector2 angleMaximums = new Vector2(30f, 420f);
        float angle = angleMaximums.x;
        while (true) {

            angle += 15 * Time.deltaTime;
            angle = Mathf.Clamp(angle,angleMaximums.x, angleMaximums.y);
            cameraRef.transform.Rotate(new Vector3(0, 0, angle * Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }
    }

    void OnDestroy() {
        AudioManager.instance.SetLoop(false);
        AudioManager.instance.StopSound();
    }
}
