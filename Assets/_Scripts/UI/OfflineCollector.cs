using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OfflineCollector : MonoBehaviour
{
    public void CollectOfflineScore()
    {
        DateTime l_lastDateTime = ServiceLocator.GetService<ISaveLoadService>(EServiceTypes.SaveService).GetCloseDateTime();
        IScoreService l_scoreService = ServiceLocator.GetService<IScoreService>(EServiceTypes.ScoreService);
        int l_seconds = (int)(DateTime.Now - l_lastDateTime).TotalSeconds;
        int l_offlineScore = l_scoreService.GetOfflineScore();
        l_scoreService.UpdateScore(l_seconds * l_offlineScore);
    }
}
