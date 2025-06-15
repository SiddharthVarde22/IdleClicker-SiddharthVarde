using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveData
{
    public int Score, ScoreMultiplier, AutoScore, OfflineScore;
    public long BinaryDateTime;

    public SaveData()
    {
        Score = 0;
        ScoreMultiplier = 1;
        AutoScore = 0;
        OfflineScore = 0;
        BinaryDateTime = 0;
    }
}
