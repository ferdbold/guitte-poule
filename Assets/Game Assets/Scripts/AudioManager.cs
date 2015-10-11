using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Message;

public class AudioManager : MonoBehaviour {

    static public AudioManager instance;

    public AudioSource audioSource;
    private AudioClip aud = new AudioClip();
    private bool isRecording = false;
    private message testClip;

    void Awake() {
        if (instance == null) {
            instance = this;
            SetUpDictionnary();
        }
    }

    void OnDestroy() {
        if (instance == this) {
            instance = null;
        }
    }

    void Update() {
        if (Input.GetKeyDown("c")) {
            startRecord();
        }
        if (Input.GetKeyUp("c")) {
            testClip = getRecordMessage();
        }
        if (Input.GetKeyDown("v")) {
            NetManager.instance.SendMessage(testClip);
            PlaySoundFromMessage(testClip);
        }
    }

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public AudioClip[] audioClipArray;
    Dictionary<string, AudioClip> audioDictionnary;

    /// <summary>
    /// Set up the dictionary to connect the name to the audio clip
    /// </summary>
    void SetUpDictionnary() {
        for (int i = 0; i < audioClipArray.Length; i++) {
            if (audioClipArray[i] != null) {
                audioDictionnary.Add(audioClipArray[i].name, audioClipArray[i]);
            }
        }
    }

    /// <summary>
    /// Plays an audio clip if no other clip is playing
    /// </summary>
    /// <param name="name"></param>
    public void PlayAudioClip (string name) {
        if (audioDictionnary.ContainsKey(name)) {
            if (!audioSource.isPlaying) {
                audioSource.clip = audioDictionnary[name];
                audioSource.Play();
                return;
            }
        }

        Debug.Log("Audio Clip used does not exists");
    }

    /// <summary>
    /// Plays an audio clip
    /// </summary>
    /// <param name="audioClip"></param>
    public void PlayAudioClip(AudioClip audioClip) {
        if (audioClip != null) {
            if (!audioSource.isPlaying) {
                audioSource.clip = audioClip;
                audioSource.Play();
                return;
            }
        }

        Debug.Log("Audio Clip used does not exists");
    }

    /// <summary>
    /// Plays an audio clip even if a clip is already playing (overwriting it)
    /// </summary>
    /// <param name="name"></param>
    public void PlayAudioClipOVERWRITE (string name) {
        if (audioDictionnary.ContainsKey(name)) {
            audioSource.clip = audioDictionnary[name];
            audioSource.Play();
            return;
        }

        Debug.Log("Audio Clip used does not exists");
    }

    /// <summary>
    /// Plays an audio clip
    /// </summary>
    /// <param name="audioClip"></param>
    public void PlayAudioClipOVERWRITE(AudioClip audioClip) {
        if (audioClip != null) {
            audioSource.clip = audioClip;
            audioSource.Play();
            return;
        }

        Debug.Log("Audio Clip used does not exists");
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void startRecord() {
        aud = Microphone.Start("", false, 3, 8996);
        isRecording = true;
    }

    public message getRecordMessage() {
        if (isRecording) {
            Microphone.End("");
            isRecording = false;
        }
        return MakeMessageFromClip();
    }

    public message MakeMessageFromClip() {
        message sound = new message("sendSound");

        AudioSource speaker = GetComponent<AudioSource>();
        speaker.clip = aud;
        float[] samples = new float[speaker.clip.samples * speaker.clip.channels];
        speaker.clip.GetData(samples, 0);
        int cpt = 0;
        NetObject subSound = new NetObject("subSound");
        subSound.addInt("", samples.Length);

        for (int i = 0; i < samples.Length; i++) {
            if (cpt == 250) {
                sound.addNetObject(subSound);
                subSound = new NetObject("subSound");
                cpt = 0;
            }
            subSound.addFloat("", (Mathf.Floor(samples[i] * 1000) / 1000));
            cpt++;
        }
        return sound;
    }

    public void PlaySoundFromMessage(message sound) {
        AudioSource speaker = GetComponent<AudioSource>();
        float[] samples = new float[sound.getNetObject(0).getInt(0)];
        for (int i = 0; i < sound.getNetObjectCount(); i++) {
            NetObject subSound = sound.getNetObject(i);
            for (int j = 0; j < subSound.getFloatCount(); j++) {
                samples[(i * 250) + j] = subSound.getFloat(j);
            }
        }
        speaker.clip = aud;
        speaker.clip.SetData(samples, 0);
        speaker.Play();
    }
}