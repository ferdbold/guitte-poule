using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameSave {

    //STATIC INSTANCE
    public static GameSave current;

    //DATA IN A SAVE : 
    public int lenght = 0;
    public bool hasPlantedSeed = false;

    /// <summary> Creates a new gamesave from the data of the persistent managers </summary>
    public GameSave() {
        lenght = 0;
        hasPlantedSeed = false;
    }

    /// <summary> Creates a new gamesave from nothing </summary>
    public GameSave(bool createNew) {
        lenght = 0;
        hasPlantedSeed = false;
    }

}
