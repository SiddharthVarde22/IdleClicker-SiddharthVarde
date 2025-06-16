using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class FloatingText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_floatingNumberText;
    [SerializeField] RectTransform m_rectTransform;
    const float m_floatingHeight = 300, m_animDuration = 0.5f;
    Action m_onAnimComplete;

    public void Initialize(int a_score, Action a_animComplete)
    {
        m_floatingNumberText.SetText(a_score.ToString());
        m_onAnimComplete = a_animComplete;
        m_rectTransform.DOAnchorPosY(m_rectTransform.anchoredPosition.y + m_floatingHeight, m_animDuration).OnComplete(OnAnimationCompleted);
    }
    private void OnAnimationCompleted()
    {
        m_onAnimComplete.Invoke();
    }
}
