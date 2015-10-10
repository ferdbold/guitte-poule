using UnityEngine;
using System.Collections;
using DG.Tweening;

public class RotateTransform : MonoBehaviour {

    public Vector3 rotationToDo;
    public float rotationTime;

	// Use this for initialization
	void Start () {
        transform.DORotate((transform.rotation.eulerAngles + rotationToDo), rotationTime);
	}
	
}
