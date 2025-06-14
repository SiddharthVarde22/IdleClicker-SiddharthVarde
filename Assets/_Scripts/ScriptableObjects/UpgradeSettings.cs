using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="UpgradeSettingsSO", menuName = "Upgrades/Settings")]
public class UpgradeSettings : ScriptableObject
{
    [SerializeField] int m_numberToIncrease = 1, m_costToIncrease = 100;

    public int NumberToIncrease { get { return m_numberToIncrease; } }
    public int CostToIncrease { get { return m_costToIncrease; } }
}
