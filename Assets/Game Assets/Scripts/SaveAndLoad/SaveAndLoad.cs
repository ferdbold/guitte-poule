using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveAndLoad {

    public static GameSave savedGame;


    public static void Save() {
        //Debug.Log("Saving at Destination : " + Application.persistentDataPath); //Debug Save Path

        //Sava data on disk
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGame.Tendresse"); //filename
        bf.Serialize(file, SaveAndLoad.savedGame);
        file.Close();

    }

    public static void Load() {

        //Debug.Log("Loading at Destination : " + Application.persistentDataPath); //Debug Save Path
        if (File.Exists(Application.persistentDataPath + "/savedGame.Tendresse")) {
            Debug.Log("File exists!");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGame.Tendresse", FileMode.Open);
            SaveAndLoad.savedGame = (GameSave)bf.Deserialize(file);
            GameSave.current = SaveAndLoad.savedGame;
            file.Close();
        } else {
            Debug.Log("File does not exists!");
            savedGame = new GameSave();
            GameSave.current = savedGame;
        }
    }

    public static void DeleteSaves() {
        if (File.Exists(Application.persistentDataPath + "/savedGame.Tendresse")) {
            File.Delete(Application.persistentDataPath + "/savedGame.Tendresse");
        }
        savedGame = new GameSave();
        GameSave.current = savedGame;

    }

}
