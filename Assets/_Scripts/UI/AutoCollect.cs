using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCollect : MonoBehaviour
{
    int m_amountToIncrease = 0;
    float m_currentElapsedTime = 0;
    const float AUTO_COLLECT_TIME = 1;
    IScoreService m_scoreService;

    private void Start()
    {
    }
    public void ToggleAutoCollect(bool a_isEnable)
    {
        enabled = a_isEnable;
        if (a_isEnable)
        {
            if (m_scoreService == null)
            {
                m_scoreService = ServiceLocator.GetService<IScoreService>(EServiceTypes.ScoreService);
            }
            m_currentElapsedTime = 0;
        }
    }
    public void UpdateAutoCollectAmount(int a_amount)
    {
        m_amountToIncrease = a_amount;
    }

    private void Update()
    {
        m_currentElapsedTime += Time.deltaTime;
        if(m_currentElapsedTime >= AUTO_COLLECT_TIME)
        {
            IncreaseScore();
        }
    }
    private void IncreaseScore()
    {
        m_currentElapsedTime -= AUTO_COLLECT_TIME;
        m_scoreService.UpdateScore(m_amountToIncrease);
    }
}
