using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameplayHud : MonoBehaviour, IGameplayHud
{
    [SerializeField] Canvas m_canvas;
    [SerializeField] GraphicRaycaster m_graphicsRaycaster;
    [SerializeField] TextMeshProUGUI m_scoreText;

    IScoreService m_scoreService;

    private void Start()
    {
        m_scoreService = ServiceLocator.GetService<IScoreService>(EServiceTypes.ScoreService);
        m_scoreService.SubscribeForScoreIncreaseEvent(OnScoreIncresed);
        SetScoreText(m_scoreService.GetScore());
    }
    private void OnDestroy()
    {
        m_scoreService.UnSubscribeFromScoreIncreaseEvent(OnScoreIncresed);
        m_scoreService = null;
    }

    public void ShowCanvas()
    {
        ToggleCanvas(true);
    }
    public void HideCanvas()
    {
        ToggleCanvas(false);
    }
    private void ToggleCanvas(bool a_isVisible)
    {
        m_canvas.enabled = a_isVisible;
        m_graphicsRaycaster.enabled = a_isVisible;
    }
    private void OnScoreIncresed(int a_score)
    {
        SetScoreText(a_score);
    }
    private void SetScoreText(int a_score)
    {
        m_scoreText.SetText($"Points - {a_score}");
    }
}
