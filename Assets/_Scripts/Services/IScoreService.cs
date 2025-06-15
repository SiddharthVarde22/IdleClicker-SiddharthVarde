using System;

public interface IScoreService
{
    public void UpdateScore(int a_score);
    public void IncreaseScoreMultiplier(int a_multiplier);
    public int GetScore();
    public int GetScoreMultiplier();
    public void SubscribeForScoreIncreaseEvent(Action<int> a_callback);
    public void UnSubscribeFromScoreIncreaseEvent(Action<int> a_functionToRemove);
    public void SubscribeForScoreMultiplierIncreased(Action<int> a_callback);
    public void UnSubscribeFromScoreMultiplier(Action<int> a_functionToRemove);
    public void IncreaseAutocollectScore(int a_aotoCollectScore);
    public int GetAutocollectScore();
    public int GetOfflineScore();
    public void IncreaseOfflineScore(int a_offlineScore);
}
