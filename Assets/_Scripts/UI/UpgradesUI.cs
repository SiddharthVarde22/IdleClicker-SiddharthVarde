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
    [SerializeField] AutoCollect m_autoCollector;
    [SerializeField] OfflineCollector m_offlineCollectorClass;

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
        m_autoCollection.GetButton().onClick.AddListener(OnUpgradeAutoCollectClicked);
        m_offlineCollector.GetButton().onClick.AddListener(OnUpgradeOfflineCollectorClicked);
        if(m_scoreService.GetAutocollectScore() > 0)
        {
            UpdateAutoCollectState();
        }
        if(m_scoreService.GetOfflineScore() > 0)
        {
            m_offlineCollectorClass.CollectOfflineScore();
        }
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
        int l_currentScore = m_scoreService.GetScore();
        ToggleScoreMultiplierbutton(l_currentScore);
        ToggleAutocollectButton(l_currentScore);
        ToggleOfflineCollectionButton(l_currentScore);
        m_scoreService.SubscribeForScoreIncreaseEvent(ToggleScoreMultiplierbutton);
        m_scoreService.SubscribeForScoreIncreaseEvent(ToggleAutocollectButton);
        m_scoreService.SubscribeForScoreIncreaseEvent(ToggleOfflineCollectionButton);
        ToggleCanvas(true);
    }
    public void HideCanvas()
    {
        ToggleCanvas(false);
        m_scoreService.UnSubscribeFromScoreIncreaseEvent(ToggleScoreMultiplierbutton);
        m_scoreService.UnSubscribeFromScoreIncreaseEvent(ToggleAutocollectButton);
        m_scoreService.UnSubscribeFromScoreIncreaseEvent(ToggleOfflineCollectionButton);
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
    private void OnUpgradeAutoCollectClicked()
    {
        if(m_upgradeService.UpgradeAutoCollector())
        {
            ToggleAutocollectButton(m_scoreService.GetScore());
            UpdateAutoCollectState();
        }
    }
    public void ToggleAutocollectButton(int a_currentScore)
    {
        m_autoCollection.Updatebutton(m_upgradeService.GetRequiredAutoCollectAmount(), a_currentScore);
    }
    public void UpdateAutoCollectState()
    {
        m_autoCollector.ToggleAutoCollect(true);
        m_autoCollector.UpdateAutoCollectAmount(m_scoreService.GetAutocollectScore());
    }
    private void OnUpgradeOfflineCollectorClicked()
    {
        if(m_upgradeService.UpgradeOfflineCollector())
        {
            ToggleOfflineCollectionButton(m_scoreService.GetScore());
        }
    }
    private void ToggleOfflineCollectionButton(int a_currentScore)
    {
        m_offlineCollector.Updatebutton(m_upgradeService.GetRequiredOfflineCollectionAmount(), a_currentScore);
    }
}
