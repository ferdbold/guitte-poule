using UnityEngine;
using System.Collections;

public class SkyScript : MonoBehaviour {

    public float height = 5f;
    private bool _isActive = false;

    public void StartHeight() {
        StartCoroutine(SetHeight(1));
    }

    IEnumerator SetHeight(float distance) {
        _isActive = true;
        yield return null;
    }
}
