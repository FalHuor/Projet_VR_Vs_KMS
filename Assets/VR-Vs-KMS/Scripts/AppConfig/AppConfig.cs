﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AppConfig {
    // HTC, force the device to HTC, PC, force device to PC Keyboard, AUTO: select the device according to the hardware connected.
    public string DeviceUsed = "AUTO";
    public int LifeNumber = 10; 
    public double DelayShoot = 1.5; 
    public int DelayTeleport = 5; 
    public string ColorShotKMS = "Green"; 
    public string ColorShotVirus = "Red"; 
    public int NbContaminatedPlayerToVictory = 10; 
    public int RadiusExplosion = 3; 
    public int TimeToAreaContamination = 4; 


    private static AppConfig inst;

    public static AppConfig Inst
    {
        get
        {
            if (inst == null) inst = new AppConfig();
            return inst;
        }
    }

    /// <summary>
    /// Update the values from a Json String
    /// </summary>
    /// <param name="jsonString"></param>
    public void UpdateValuesFromJsonString(string jsonString)
    {
        JsonUtility.FromJsonOverwrite(jsonString, Inst);
    }


    /// <summary>
    /// Read from a file path the JSON for deserializing
    /// use this 
    /// Appli
    /// </summary>
    /// <param name="filePath"></param>
    public void UpdateValuesFromJsonFile(string filePath)
    {
        UpdateValuesFromJsonString(System.IO.File.ReadAllText(filePath));
    }

    /// <summary>
    /// Read from the json file called StreamingAssets/{Application.productName+".AppConfig.json"}
    /// </summary>
    public void UpdateValuesFromJsonFile()
    {
        Debug.Log(Application.absoluteURL);
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "GameConfig.json");
        Debug.Log("path : " + path);
        UpdateValuesFromJsonFile(path);
    }

    public string ToJsonString()
    {
        return JsonUtility.ToJson(Inst, true);
    }

}
