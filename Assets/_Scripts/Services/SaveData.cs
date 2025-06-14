using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveData
{
    public int Score, ScoreMultiplier, AutoScore;

    public SaveData()
    {
        Score = 0;
        ScoreMultiplier = 1;
        AutoScore = 0;
    }
}
