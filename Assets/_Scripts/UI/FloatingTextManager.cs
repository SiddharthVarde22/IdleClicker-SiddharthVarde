using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    [SerializeField] FloatingText m_floatingTextPrefab;

    ObjectPool<FloatingText> m_floatingTextPool;
    IScoreService m_scoreService;
    const int MinimumFloatTextCount = 10;

    private void Start()
    {
        m_scoreService = ServiceLocator.GetService<IScoreService>(EServiceTypes.ScoreService);
        m_floatingTextPool = new ObjectPool<FloatingText>(MinimumFloatTextCount, CreateNewFloatingText,
            OnGetFloatingTextCalled, OnReleaseFloatingText, OnDestroyFloatingText);
    }

    private FloatingText CreateNewFloatingText()
    {
        FloatingText l_floatingText = Instantiate<FloatingText>(m_floatingTextPrefab, transform);
        return l_floatingText;
    }
    private void OnGetFloatingTextCalled(FloatingText a_floatingText)
    {

    }
    private void OnReleaseFloatingText(FloatingText a_floatingtext)
    {

    }
    private void OnDestroyFloatingText(FloatingText a_floatingText)
    {

    }
}
