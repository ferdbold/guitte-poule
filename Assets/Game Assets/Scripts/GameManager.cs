using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Message;
using Tendresse.Data;

public class GameManager : MonoBehaviour {

    static public GameManager instance;
    public bool isFirst; //TODO :The server chooses a first and second player in the date. THIS DOES NOT CHANGE DURING THE DATE !

    private AudioClip aud = new AudioClip();
    private bool isRecording = false;
    private message testClip;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        LoadData();
    }

    void OnDestroy() {
        if (instance == this) {
            instance = null;
            SaveData();
           
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------
    //////////////////////////////////////////////// SAVE AND LOAD ///////////////////////////////////////////////////////////


    public void LoadData() {
        SaveAndLoad.Load();
        Debug.Log("Length : " + SaveAndLoad.savedGame.lenght);
    }

    public void SaveData() {
        SaveAndLoad.Save();
        Debug.Log("Length : " + SaveAndLoad.savedGame.lenght);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)){
            SaveAndLoad.DeleteSaves();
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            SaveAndLoad.savedGame.lenght++;
            Debug.Log("Length : " + SaveAndLoad.savedGame.lenght);
        }

        if (Input.GetKeyDown("c"))
        {
            startRecord();
        }
        if (Input.GetKeyUp("c"))
        {
            testClip = getRecordMessage();
        }
        if (Input.GetKeyDown("v"))
        {
            NetManager.instance.SendMessage(testClip);
            PlaySoundFromMessage(testClip);
        }
    }
    

    //-----------------------------------------------------------------------------------------------------------------------
    //////////////////////////////////////////////// SWITCH SCENE ///////////////////////////////////////////////////////////

    //VARIABLES
    Scenes currentScene;

    public enum Scenes {
        Menu,
        LoadingGame,
        Main
    }

    //FUNCTIONS

    /// <summary>
    /// Switch between scenes
    /// </summary>
    /// <param name="scene"></param>
    public void SwitchScene(Scenes scene) {
        OnSceneEnd(currentScene);
        currentScene = scene;
        OnSceneStartup(currentScene);
    }

    /// <summary>
    /// Event when the scene end
    /// </summary>
    /// <param name="scene"></param>
    void OnSceneEnd(Scenes scene) {
        switch (scene) {
            case Scenes.Menu:
                GameObject.FindWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().mainMenu.DOFade(0, 0.75f);
                break;
            case Scenes.LoadingGame:
                CanvasGroup loading = GameObject.FindWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().loading;
                loading.DOFade(0, 0.75f);
                loading.GetComponent<LoadingUI>().StopAllCoroutines();
                break;
            case Scenes.Main:
                break;
        }
    }

    /// <summary>
    /// Event on scene startup
    /// </summary>
    /// <param name="scene"></param>
    void OnSceneStartup(Scenes scene) {
        switch (scene) {
            case Scenes.Menu:
                Application.LoadLevel("Menu");
                break;
            case Scenes.LoadingGame:
                CanvasGroup loading = GameObject.FindWithTag("MainMenuRef").GetComponent<MainMenuRefUI>().loading;
                loading.DOFade(1, 0.75f);
                loading.GetComponent<LoadingUI>().StartAnim();
                message messa = new message("queueMatch");
                NetManager.instance.SendMessage(messa);
                break;
            case Scenes.Main:
                Application.LoadLevel("Main");
                break;
        }
    }

//-----------------------------------------------------------------------------------------------------------------------
////////////////////////////////////////////////              ///////////////////////////////////////////////////////////

    /// <summary>
    /// Event when the player finds a partner online
    /// </summary>
    public void Event_OnFindPartner(bool isFirst) {
        SwitchScene(Scenes.Main);
    }


    public void startRecord()
    {
        aud = Microphone.Start("", false, 3, 8996);
        isRecording = true;
    }

    public message getRecordMessage()
    {
        if (isRecording)
        {
            Microphone.End("");
            isRecording = false;
        }
        return MakeMessageFromClip();
    }

    public message MakeMessageFromClip()
    { 
        message sound = new message("sendSound");

        AudioSource speaker = GetComponent<AudioSource>();
        speaker.clip = aud;
        float[] samples = new float[speaker.clip.samples * speaker.clip.channels];
        speaker.clip.GetData(samples, 0);
        int cpt=0;
        NetObject subSound = new NetObject("subSound");
        subSound.addInt("", samples.Length);

        for(int i=0; i<samples.Length;i++)
        {
            if (cpt == 250)
            {
                sound.addNetObject(subSound);
                subSound = new NetObject("subSound");
                cpt = 0;
            }
            subSound.addFloat("", (Mathf.Floor(samples[i]*1000)/1000));
            cpt++;
        }
        Debug.Log(conversionTools.convertMessageToString(sound));
        return sound;
    }

    public void PlaySoundFromMessage(message sound)
    {
        AudioSource speaker = GetComponent<AudioSource>();
        float[] samples = new float[sound.getNetObject(0).getInt(0)];
        for (int i = 0; i < sound.getNetObjectCount(); i++)
        {
            NetObject subSound= sound.getNetObject(i);
            for (int j = 0; j < subSound.getFloatCount(); j++)
            {
                samples[(i * 250) + j] = subSound.getFloat(j);
            }
        }
        speaker.clip = aud;
        speaker.clip.SetData(samples, 0);
        speaker.Play();
    }




    public void Event_OnSendImage(TendresseData tData) {
        Debug.Log("Beginning Send Message");
        message mes = NetManager.instance.MakeMessageFromImage(tData);
        Debug.Log("Created Message" + conversionTools.convertMessageToString(mes));
        NetManager.instance.SendMessage(mes);
        Debug.Log("Sent Message");
    }

    public void Event_OnReceiveImage(TendresseData tData) {
        Debug.Log("draw 1");
        DateManager.instance.DrawImageAt(tData, Vector3.zero, 1f);
    }

    

}
