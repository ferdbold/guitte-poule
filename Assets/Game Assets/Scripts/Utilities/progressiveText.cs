using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class progressiveText : MonoBehaviour {

    public float timePerLetter = 0.1f;
    public string textToShow;

    private Text text;

	void Start () {
        text = GetComponent<Text>();
        StartCoroutine(ShowText());
	}
	
    IEnumerator ShowText() {
        text.text = "";
        for (int i = 0; i < textToShow.Length; i++) {
            text.text += textToShow[i];
            yield return new WaitForSeconds(timePerLetter);
        }
    }
	
}
