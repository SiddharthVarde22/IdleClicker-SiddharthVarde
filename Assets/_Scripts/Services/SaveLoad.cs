using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[DefaultExecutionOrder(-80)]
public class SaveLoad : MonoBehaviour, ISaveLoadService, IGameService
{
    SaveData m_saveData;
    const string FILE_NAME = "SaveData.json";
    string m_fullPath;

    private void Awake()
    {
        m_fullPath = Application.persistentDataPath + "/" + FILE_NAME;
        ReadDataFromFile();
    }
    private void OnEnable()
    {
        RegisterService();
    }
    public void RegisterService()
    {
        ServiceLocator.RegisterService(EServiceTypes.SaveService, this);
    }

    public void UpdateAutoCollectScoreData(int a_newData)
    {
        m_saveData.AutoScore = a_newData;
    }

    public void UpdateScoreData(int a_newScore)
    {
        m_saveData.Score = a_newScore;
    }

    public void UpdateScoreMultiplierData(int a_newmultiplier)
    {
        m_saveData.ScoreMultiplier = a_newmultiplier;
    }
    private void WriteDataToFile()
    {
        string l_jsonData = JsonUtility.ToJson(m_saveData);
        File.WriteAllText(m_fullPath, l_jsonData);
    }
    private void ReadDataFromFile()
    {
        if (File.Exists(m_fullPath))
        {
            string l_data = File.ReadAllText(m_fullPath);
            m_saveData = (SaveData)JsonUtility.FromJson(l_data, typeof(SaveData));
        }
        else
        {
            m_saveData = new SaveData();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            //SetDateTime();
            WriteDataToFile();
        }
    }
    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            //SetDateTime();
            WriteDataToFile();
        }
    }
    private void OnApplicationQuit()
    {
        SetDateTime();
        WriteDataToFile();
    }

    public int GetScoreData()
    {
        return m_saveData.Score;
    }

    public int GetMultiplier()
    {
        return m_saveData.ScoreMultiplier;
    }

    public int GetAutoScore()
    {
        return m_saveData.AutoScore;
    }

    public DateTime GetCloseDateTime()
    {
        return DateTime.FromBinary(m_saveData.BinaryDateTime);
    }

    private void SetDateTime()
    {
        if (m_saveData.OfflineScore > 0)
        {
            m_saveData.BinaryDateTime = DateTime.Now.ToBinary();
        }
    }

    public int GetOfflineScore()
    {
        return m_saveData.OfflineScore;
    }

    public void UpdateOfflineScore(int a_offlineScore)
    {
        m_saveData.OfflineScore = a_offlineScore;
    }
}
