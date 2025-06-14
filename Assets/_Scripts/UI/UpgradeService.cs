using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeService : MonoBehaviour, IUpgradeService, IGameService
{
    [SerializeField] UpgradeSettings m_ScoreMultiplierSO, m_autocollectScoreSO;

    IScoreService m_scoreService;

    private void Awake()
    {
        RegisterService();
        m_scoreService = ServiceLocator.GetService<IScoreService>(EServiceTypes.ScoreService);
    }
    public void RegisterService()
    {
        ServiceLocator.RegisterService(EServiceTypes.UpgradeService, this);
    }

    public bool UpgradeAutoCollector()
    {
        int l_requiredAmount = GetRequiredAutoCollectAmount();
        if(l_requiredAmount <= m_scoreService.GetScore())
        {
            m_scoreService.IncreaseAutocollectScore(m_autocollectScoreSO.NumberToIncrease);
            m_scoreService.UpdateScore(-l_requiredAmount);
            return true;
        }
        return false;
    }
    public void UpgradeOfflineCollector()
    {

    }
    public bool UpgradeScoreMultiplier()
    {
        int l_requiredAmount = GetRequiredMultiplierAmount();
        if (l_requiredAmount <= m_scoreService.GetScore())
        {
            m_scoreService.IncreaseScoreMultiplier(m_ScoreMultiplierSO.NumberToIncrease);
            m_scoreService.UpdateScore(-l_requiredAmount);
            return true;
        }
        return false;
    }
    public int GetRequiredMultiplierAmount()
    {
        return m_scoreService.GetScoreMultiplier() * m_ScoreMultiplierSO.CostToIncrease;
    }
    public int GetRequiredAutoCollectAmount()
    {
        return (m_scoreService.GetAutocollectScore() + 1) * m_autocollectScoreSO.CostToIncrease;
    }
}
