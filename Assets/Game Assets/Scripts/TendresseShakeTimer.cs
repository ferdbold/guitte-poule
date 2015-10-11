using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class TendresseShakeTimer : MonoBehaviour {

    public Text numberText;
    public Text textText;

    void OnEnable() {
        StartCoroutine(AnimateTimer());
    }

    void OnDisable() {
        StopAllCoroutines();
    }

    IEnumerator AnimateTimer() {
        textText.text = "Préparez-vous à montrer votre tendresse";
        numberText.text = "";
        yield return new WaitForSeconds(2.5f);
        numberText.text = "3";
        yield return new WaitForSeconds(1f);
        numberText.text = "2";
        yield return new WaitForSeconds(1f);
        numberText.text = "1";
        yield return new WaitForSeconds(1f);
        textText.text = "Secouez votre concombre!";
        numberText.text = "";
        yield return new WaitForSeconds(2f);
        numberText.text = "";
        yield return new WaitForSeconds(3f);
        GetComponent<CanvasGroup>().DOFade(0, 2f);
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }
}
