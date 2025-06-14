using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeService
{
    public bool UpgradeScoreMultiplier();
    public void UpgradeAutoCollector();
    public void UpgradeOfflineCollector();
    public int GetRequiredMultiplierAmount();
}
