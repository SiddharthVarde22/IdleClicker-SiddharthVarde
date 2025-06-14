using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Button m_button;
    [SerializeField] TextMeshProUGUI m_requiredAmountText;

    public void SetRequiredAmount(int a_amount)
    {
        m_requiredAmountText.SetText(a_amount.ToString());
    }
    public Button GetButton()
    {
        return m_button;
    }
    public void SetIsInteractive(bool a_isInteractive)
    {
        m_button.interactable = a_isInteractive;
    }
    public void Updatebutton(int a_requiredAmount, int a_availableAmount)
    {
        SetRequiredAmount(a_requiredAmount);
        SetIsInteractive(a_requiredAmount <= a_availableAmount);
    }
}
