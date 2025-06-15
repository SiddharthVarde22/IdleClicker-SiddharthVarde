using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ISaveLoadService
{
    public void UpdateScoreData(int a_newScore);
    public void UpdateScoreMultiplierData(int a_newmultiplier);
    public void UpdateAutoCollectScoreData(int a_newData);
    public int GetScoreData();
    public int GetMultiplier();
    public int GetAutoScore();
    public DateTime GetCloseDateTime();
    public int GetOfflineScore();
    public void UpdateOfflineScore(int a_offlineScore);
}
