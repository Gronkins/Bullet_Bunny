using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public PlayerData playerData = new PlayerData();
    
    public void SaveToJson()
    {
        string playerSavedData = JsonUtility.ToJson(playerData);
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath, playerSavedData);
        Debug.Log("Saved!");
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/PlayerData.json";

        if(System.IO.File.Exists(filePath))
        {
            string playerSavedData = System.IO.File.ReadAllText(filePath);

            playerData = JsonUtility.FromJson<PlayerData>(playerSavedData);
            Debug.Log("Game loaded");
        }
        else
        {
            Debug.Log("No save data");
            SaveToJson();
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public int stageProgress;
    public int[] carrotsCollectedPerStage = new int[GameInformation.numberOfStages + 1];
    public float[] bestTimePerStage = new float[GameInformation.numberOfStages + 1];
}
