using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SkyScript : MonoBehaviour {

    public float height = 5f;
    public float translationPerHeight = -1f;
    public bool isActive = false;
    private float animationTime = 4f;

    void Update() {
        if(isActive) {
            
            Vector3 targetPosition = new Vector3(transform.position.x,  SaveAndLoad.savedGame.lenght * translationPerHeight, transform.position.z);
            targetPosition = Vector3.Lerp(transform.position, targetPosition, 0.15f);
            //Debug.Log("Lerping to position " + targetPosition);
            transform.position = targetPosition;
        }
    }

    public void StartHeight() {
        StartCoroutine(SetHeight(SaveAndLoad.savedGame.lenght * translationPerHeight));
    }

    public void MakeShakeActive() {
        isActive = true;
    }

    public void MakeShakeUnactive() {
        isActive = false;
    }

    IEnumerator SetHeight(float distance) {
        Debug.Log("Setting height");
        transform.DOMoveY(distance, animationTime);
        yield return new WaitForSeconds(animationTime);
        
        yield return null;
    }
}
