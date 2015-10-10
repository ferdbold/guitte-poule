using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TiltShakeMotor : MonoBehaviour {

    public HingeJoint2D hingeMotor;
    public float motorForce;

    [Header("Animation")]
    public AnimationCurve animCurve;
    public float targetPosition = -5f;
    public float startPosition = -15f;
    public float animationTime = 2.5f;


    private Vector3 tiltInputs = new Vector3(0, 0, 0);
    private bool inAnim = false; //Is the cucumber in animation
    
    
	// Use this for initialization
	void Start () {
        if(SaveAndLoad.savedGame.hasPlantedSeed) {
            StartCoroutine(CucumberAnimation());
            GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().ShowPlayButton();
            GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().HideIntroButton();
        } else {
            hingeMotor.connectedAnchor = new Vector2(0, startPosition);
            GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().HidePlayButton();
            GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().ShowIntroButton();
        }
    }
	
	// Update is called once per frame
	void Update () {
        TiltCucumber();
    }

    void TiltCucumber() {
        if (!inAnim) {
            tiltInputs = Input.acceleration;
            //Debug.Log(tiltInputs);

            JointMotor2D motor = hingeMotor.motor;
            motor.motorSpeed = (tiltInputs.x * motorForce);
            hingeMotor.motor = motor;
        }
    }


    public void PlantSeed() {
        StartCoroutine(PlantSeedAnimation());
    }

    IEnumerator PlantSeedAnimation() {
        GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().HideIntroButton();
        yield return new WaitForSeconds(3f);
        yield return(StartCoroutine(CucumberAnimation()));
        GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().ShowPlayButton();
    }

    IEnumerator CucumberAnimation() {
        inAnim = true;
        float dist = startPosition;
        for (float i = 0; i < 1f; i += Time.deltaTime/ animationTime) {
            dist = Mathf.Lerp(startPosition, targetPosition, animCurve.Evaluate(i));
            hingeMotor.connectedAnchor = new Vector2(0, dist);
            yield return null;
        }
        inAnim = false;
        hingeMotor.connectedAnchor = new Vector2(0, targetPosition);
        SaveAndLoad.savedGame.hasPlantedSeed = true;
        
    }

}
