using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreService : IScoreService, IGameService
{
    int m_score, m_scoreMultiplier;
    Action<int> m_scoreIncreaseHandler;
    Action<int> m_scoreMultiplierHandler;
    public ScoreService()
    {
        RegisterService();
        m_score = 0; // read from saved value
        m_scoreMultiplier = 1; // read from saved value
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
}
