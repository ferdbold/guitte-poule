using UnityEngine;
using System.Collections;

public class TiltShakeMotor : MonoBehaviour {

    public HingeJoint2D hingeMotor;
    public float motorForce;

    private Vector3 tiltInputs = new Vector3(0, 0, 0);
    
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        tiltInputs = Input.acceleration;
        //Debug.Log(tiltInputs);

        JointMotor2D motor = hingeMotor.motor;
        motor.motorSpeed = (tiltInputs.x * motorForce);
        hingeMotor.motor = motor;
    }
}
