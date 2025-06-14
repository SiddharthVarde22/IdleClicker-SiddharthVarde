using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class UpgradesUI : MonoBehaviour
{
    [SerializeField] Canvas m_canvas;
    [SerializeField] GraphicRaycaster m_raycaster;
    [SerializeField] Button m_closeButton, m_UpgradesButton;
    [SerializeField] UpgradeButton m_scoreMultiplier, m_offlineCollector, m_autoCollection;
    [SerializeField] float m_startPosition, m_endPostion, m_duration = 0.2f;

    IUpgradeService m_upgradeService;
    IScoreService m_scoreService;

    private void Start()
    {
        m_closeButton.interactable = false;
        m_UpgradesButton.onClick.AddListener(InTransitionAnim);
        m_closeButton.onClick.AddListener(OutTransitionAnim);
        m_upgradeService = ServiceLocator.GetService<IUpgradeService>(EServiceTypes.UpgradeService);
        m_scoreService = ServiceLocator.GetService<IScoreService>(EServiceTypes.ScoreService);
        m_scoreMultiplier.GetButton().onClick.AddListener(OnUpgradeScoreMultiplierClicked);
    }
    private void OnDestroy()
    {
        m_upgradeService = null;
    }
    private void ToggleCanvas(bool a_isVisible)
    {
        m_canvas.enabled = a_isVisible;
        m_raycaster.enabled = a_isVisible;
    }
    public void ShowCanvas()
    {
        ToggleScoreMultiplierbutton(m_scoreService.GetScore());
        m_scoreService.SubscribeForScoreIncreaseEvent(ToggleScoreMultiplierbutton);
        ToggleCanvas(true);
    }
    public void HideCanvas()
    {
        ToggleCanvas(false);
        m_scoreService.UnSubscribeFromScoreIncreaseEvent(ToggleScoreMultiplierbutton);
    }
    public async void InTransitionAnim()
    {
        m_UpgradesButton.interactable = false;
        ShowCanvas();
        transform.DOMoveX(m_endPostion, m_duration);
        await Task.Delay((int)(m_duration * 1000));
        m_closeButton.interactable = true;
    }
    private async void OutTransitionAnim()
    {
        m_closeButton.interactable = false;
        transform.DOMoveX(m_startPosition, m_duration);
        await Task.Delay((int)(m_duration * 1000));
        HideCanvas();
        m_UpgradesButton.interactable = true;
    }
    private void OnUpgradeScoreMultiplierClicked()
    {
        if (m_upgradeService.UpgradeScoreMultiplier())
        {
            ToggleScoreMultiplierbutton(m_scoreService.GetScore());
        }
    }
    public void ToggleScoreMultiplierbutton(int a_currentScore)
    {
        m_scoreMultiplier.Updatebutton(m_upgradeService.GetRequiredMultiplierAmount(), a_currentScore);
    }
}
