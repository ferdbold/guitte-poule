using UnityEngine;
using System.Collections;

public class DestroyOnTimer : MonoBehaviour {

    public float time;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, time);
	}

}
