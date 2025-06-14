using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class TapButton : MonoBehaviour
{
    [SerializeField] Button m_tapButton;
    [SerializeField] TextMeshProUGUI m_multiplierText;
    [SerializeField] Vector3 m_punchAnimDirection = new Vector3(1, 1, 0);
    [SerializeField] float m_punchScale = 1.5f, m_punchDuration = 0.1f;

    IScoreService m_scoreService;
    private void Start()
    {
        m_tapButton.onClick.AddListener(OnClickPerformed);
        m_scoreService = ServiceLocator.GetService<IScoreService>(EServiceTypes.ScoreService);
        UpdateMultiplierText(m_scoreService.GetScoreMultiplier());
        m_scoreService.SubscribeForScoreMultiplierIncreased(OnScoreMultiplierIncreased);
    }
    private void OnDestroy()
    {
        m_scoreService.UnSubscribeFromScoreMultiplier(OnScoreMultiplierIncreased);
        m_scoreService = null;
    }

    private void OnClickPerformed()
    {
        if (DOTween.IsTweening(transform))
            DOTween.Complete(transform);

        transform.DOPunchScale(m_punchAnimDirection * m_punchScale, m_punchDuration);
        m_scoreService.UpdateScore(m_scoreService.GetScoreMultiplier());
    }

    private void UpdateMultiplierText(int a_multiplier)
    {
        m_multiplierText.SetText(a_multiplier.ToString());
    }
    private void OnScoreMultiplierIncreased(int a_multiplier)
    {
        UpdateMultiplierText(a_multiplier);
    }
}
