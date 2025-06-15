using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeService
{
    public bool UpgradeScoreMultiplier();
    public bool UpgradeAutoCollector();
    public bool UpgradeOfflineCollector();
    public int GetRequiredMultiplierAmount();
    public int GetRequiredAutoCollectAmount();
    public int GetRequiredOfflineCollectionAmount();
}
