using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainIntro : MonoBehaviour {

    public Text description;

    void Awake() {
        StartWritingAnim();
    }

    public void StartWritingAnim() {

    }

    IEnumerator WritingAnim() {
        yield return null;   
    }
}
