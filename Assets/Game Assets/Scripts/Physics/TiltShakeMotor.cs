using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TiltShakeMotor : MonoBehaviour {

    public HingeJoint2D hingeMotor;
    public float motorForce;
    public GameObject SeedFallingObject;
    public GameObject SeedGrowAnimation;

    [Header("Animation")]
    public AnimationCurve animCurve;
    public float targetPosition = -5f;
    public float startPosition = -15f;
    public float animationTime = 2.5f;

    [Header("Tilt")]
    public bool isInTendresse = false;
    private Vector3 previousTiltInputs = new Vector3(0, 0, 0);
    private Vector3 tiltInputs = new Vector3(0, 0, 0);
    private bool inAnim = false; //Is the cucumber in animation

    [Header("Sky")]
    public SkyScript skyScript;
    
    
	// Use this for initialization
	void Start () {
        InitializeCucumber();
    }


    void InitializeCucumber() {
        if (SaveAndLoad.savedGame.hasPlantedSeed) {
            if (isInTendresse) { //If Tendresse Mode
                StartCoroutine(CucumberAnimation());
                GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().HidePlayButton();
                GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().HideIntroButton();
                GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().HideIntroText();
                GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().TransitionToSky();
                skyScript.StartHeight();

            } else {
                GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().ShowPlayButton();
                GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().HideIntroButton();
                GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().HideIntroText();
                GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().TransitionToSky();
            }
        } else {
            hingeMotor.connectedAnchor = new Vector2(0, startPosition);
            GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().HidePlayButton();
            GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().HideIntroButton();
            StartCoroutine(SpawnPlantSeedButtonOnDelay(7f));
        }
    }

	
	// Update is called once per frame
	void Update () {
        TiltCucumber();
    }

    void TiltCucumber() {
        if (!inAnim) {
            tiltInputs = Input.acceleration;
            Vector3 diff = tiltInputs - previousTiltInputs;
            
            Debug.Log(diff);

            JointMotor2D motor = hingeMotor.motor;
          
            if (isInTendresse) {
                motor.motorSpeed = (diff.y * motorForce * 20);
            } else {
                motor.motorSpeed = (tiltInputs.x * motorForce * 2);
            }
            


            hingeMotor.motor = motor;
            previousTiltInputs = tiltInputs;
        }
    }


    public void PlantSeed() {
        GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().HideIntroText();
        StartCoroutine(PlantSeedAnimation());
    }

    IEnumerator SpawnPlantSeedButtonOnDelay(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().ShowIntroButton();
    }

    IEnumerator PlantSeedAnimation() {
        GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().HideIntroButton();
        yield return (StartCoroutine(DropSeedAndGrowPlant()));
        yield return(StartCoroutine(CucumberAnimation()));
        GameObject.FindGameObjectWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().ShowPlayButton();
    }

    IEnumerator DropSeedAndGrowPlant() {
        Instantiate(SeedFallingObject, new Vector3(0, 10, 5), Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Instantiate(SeedGrowAnimation, new Vector3(0, -5, 5), Quaternion.identity);
        yield return new WaitForSeconds(5f);


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
