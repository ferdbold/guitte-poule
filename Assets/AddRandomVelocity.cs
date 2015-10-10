using UnityEngine;
using System.Collections;

public class AddRandomVelocity : MonoBehaviour {

    private Rigidbody rb;
    public Vector3 AddedVelocity;

    public void Start() {
        rb.velocity = AddedVelocity;
    }
}
