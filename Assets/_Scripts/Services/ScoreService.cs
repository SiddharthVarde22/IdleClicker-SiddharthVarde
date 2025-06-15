using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreService : IScoreService, IGameService
{
    int m_score, m_scoreMultiplier, m_autoCollectScore, m_offlineScore;
    Action<int> m_scoreIncreaseHandler;
    Action<int> m_scoreMultiplierHandler;
    ISaveLoadService m_saveService;
    public ScoreService()
    {
        m_saveService = ServiceLocator.GetService<ISaveLoadService>(EServiceTypes.SaveService);
        RegisterService();
        m_score = m_saveService.GetScoreData();
        m_scoreMultiplier = m_saveService.GetMultiplier();
        m_autoCollectScore = m_saveService.GetAutoScore();
        m_offlineScore = m_saveService.GetOfflineScore();
    }
    ~ScoreService()
    {
        m_scoreIncreaseHandler = null;
        m_scoreMultiplierHandler = null;
    }

    public void UpdateScore(int a_score)
    {
        m_score += a_score;
        m_scoreIncreaseHandler?.Invoke(m_score);
        m_saveService.UpdateScoreData(m_score);
    }
    public int GetScore()
    {
        return m_score;
    }
    public void SubscribeForScoreIncreaseEvent(Action<int> a_callback)
    {
        m_scoreIncreaseHandler += a_callback;
    }
    public void UnSubscribeFromScoreIncreaseEvent(Action<int> a_functionToRemove)
    {
        m_scoreIncreaseHandler -= a_functionToRemove;
    }
    public void IncreaseScoreMultiplier(int a_multiplier)
    {
        m_scoreMultiplier += a_multiplier;
        m_scoreMultiplierHandler?.Invoke(m_scoreMultiplier);
        m_saveService.UpdateScoreMultiplierData(m_scoreMultiplier);
    }
    public int GetScoreMultiplier()
    {
        return m_scoreMultiplier;
    }
    public void SubscribeForScoreMultiplierIncreased(Action<int> a_callback)
    {
        m_scoreMultiplierHandler += a_callback;
    }
    public void UnSubscribeFromScoreMultiplier(Action<int> a_functionToRemove)
    {
        m_scoreMultiplierHandler -= a_functionToRemove;
    }
    public void RegisterService()
    {
        ServiceLocator.RegisterService(EServiceTypes.ScoreService, this);
    }
    public void IncreaseAutocollectScore(int a_autoColllectScore)
    {
        m_autoCollectScore += a_autoColllectScore;
        m_saveService.UpdateAutoCollectScoreData(m_autoCollectScore);
    }
    public int GetAutocollectScore()
    {
        return m_autoCollectScore;
    }

    public int GetOfflineScore()
    {
        return m_offlineScore;
    }

    public void IncreaseOfflineScore(int a_offlineScore)
    {
        m_offlineScore += a_offlineScore;
        m_saveService.UpdateOfflineScore(m_offlineScore);
    }
}
