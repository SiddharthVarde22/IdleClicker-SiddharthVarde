using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    [SerializeField] FloatingText m_floatingTextPrefab;

    ObjectPool<FloatingText> m_floatingTextPool;
    IScoreService m_scoreService;

    private void Start()
    {
        m_scoreService = ServiceLocator.GetService<IScoreService>(EServiceTypes.ScoreService);
    }
}
